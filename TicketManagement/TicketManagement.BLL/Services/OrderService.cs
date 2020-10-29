// ****************************************************************************
// <copyright file="OrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.BLL.Services
{
    internal class OrderService : IOrderService
    {
        private static readonly ConcurrentDictionary<int, string> JobIdList;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Layout> layoutRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailHelper emailHelper;
        private readonly IOrderValidator orderValidator;
        private readonly IBackgroundJobClient jobClient;
        private readonly IDataTimeHelper dataTimeHelper;
        private readonly int seatLockTimeMinutes;

        static OrderService()
        {
            JobIdList = new ConcurrentDictionary<int, string>();
        }

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<EventSeat> eventSeatRepository,
            IUserRepository userRepository,
            IEmailHelper emailHelper,
            IRepository<EventArea> eventAreaRepository,
            IRepository<Event> eventRepository,
            IRepository<Layout> layoutRepository,
            IOrderValidator orderValidator,
            IBackgroundJobClient jobClient,
            IDataTimeHelper dataTimeHelper,
            int seatLockTimeMinutes)
        {
            this.orderRepository = orderRepository;
            this.eventSeatRepository = eventSeatRepository;
            this.userRepository = userRepository;
            this.emailHelper = emailHelper;
            this.eventAreaRepository = eventAreaRepository;
            this.eventRepository = eventRepository;
            this.layoutRepository = layoutRepository;
            this.orderValidator = orderValidator;
            this.jobClient = jobClient;
            this.seatLockTimeMinutes = seatLockTimeMinutes;
            this.dataTimeHelper = dataTimeHelper;
        }

        public void AddToCart(int eventSeatId, int userId)
        {
            var newOrder = new Order()
            {
                DateUtc = this.dataTimeHelper.GetDateTimeUtcNow(),
                EventSeatId = eventSeatId,
                UserId = userId,
            };

            this.orderValidator.AddToCartValidation(newOrder);

            var eventSeat = this.eventSeatRepository.GetById(eventSeatId);
            eventSeat.State = EventSeatState.InBasket;
            this.eventSeatRepository.Update(eventSeat);

            var orderId = this.orderRepository.Create(newOrder);

            var unlockJobId = this.jobClient.Schedule<ISeatLocker>(
                x =>
                    x.UnlockSeat(orderId),
                TimeSpan.FromMinutes(this.seatLockTimeMinutes));

            JobIdList.TryAdd(eventSeatId, unlockJobId);
        }

        public void Buy(int orderId)
        {
            const string buyTicket = "Buy Ticket";
            this.orderValidator.BuyValidation(orderId);

            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            this.RemoveUnlockJob(eventSeat.Id);
            eventSeat.State = EventSeatState.Sold;
            order.DateUtc = this.dataTimeHelper.GetDateTimeUtcNow();
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Update(order);

            var eventAreaPrice = this.eventAreaRepository.GetById(eventSeat.EventAreaId).Price;
            var user = this.userRepository.GetById(order.UserId);
            user.Balance -= eventAreaPrice;
            this.userRepository.Update(user);

            // Send letter
            this.emailHelper.SendEmail(this.userRepository.GetById(order.UserId).Email, buyTicket, this.GetEventHtml(eventSeat));
        }

        public void DeleteFromCart(int orderId)
        {
            this.orderValidator.DeleteFromCartValidation(orderId);

            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            eventSeat.State = EventSeatState.Free;
            this.RemoveUnlockJob(eventSeat.Id);
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Delete(orderId);
        }

        public IEnumerable<Order> GetCartOrdersById(int userId)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.InBasket) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, DateUtc = userOrderQ.DateUtc, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).AsEnumerable();
        }

        public IEnumerable<Order> GetCartOrdersByName(string userName)
        {
            var userId = this.userRepository.FindByNormalizedUserName(userName).Id;
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.InBasket) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, DateUtc = userOrderQ.DateUtc, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).AsEnumerable();
        }

        public IEnumerable<Order> GetHistoryOrdersById(int userId)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, DateUtc = userOrderQ.DateUtc, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).AsEnumerable();
        }

        public IEnumerable<Order> GetHistoryOrdersByName(string userName)
        {
            var userId = this.userRepository.FindByNormalizedUserName(userName).Id;
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, DateUtc = userOrderQ.DateUtc, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).AsEnumerable();
        }

        private string GetEventHtml(EventSeat eventSeat)
        {
            var eventArea = this.eventAreaRepository.GetById(eventSeat.EventAreaId);
            var @event = this.eventRepository.GetById(eventArea.EventId);
            var layout = this.layoutRepository.GetById(@event.LayoutId);

            const string htmlTableStart = "<table style=\"background: #ffffff; border-radius: 3px; width: 100%;\" > " +
                                          "<tr> " +
                                          "<td style=\"box-sizing: border-box; padding: 20px;\"> " +
                                          "<table style=\"border=\"0\" cellpadding=\"0\" cellspacing=\"0\";\" > " +
                                          "<tr>" +
                                          "<td>";

            var content = $"<p>Dear client,</p>" +
                          $"Congratulations on your purchase for the {@event.Name} which started {@event.BeginDateUtc} UTC and ended {@event.EndDateUtc} UTC." +
                          $"Event description: {@event.Description}" +
                          $"Layout of the event is {layout.Name}. Description: {layout.Description}" +
                          $"Area of the event is {eventArea.Description} which is located by coordinates X: {eventArea.CoordinateX}, Y: {eventArea.CoordinateY}" +
                          $"Cost of event is {eventArea.Price}" +
                          $"Your seat is in {eventSeat.Row} row and {eventSeat.Number} number." +
                          $"<p>Good luck!</p>";

            const string htmlTableEnd = "</td> " +
                                        "</tr> " +
                                        "</table> " +
                                        "</td> " +
                                        "</tr> " +
                                        "</table>";

            var htmlBody = string.Empty;
            htmlBody += htmlTableStart;
            htmlBody += content;
            htmlBody += htmlTableEnd;

            return htmlBody;
        }

        private void RemoveUnlockJob(int eventSeatId)
        {
            if (!JobIdList.ContainsKey(eventSeatId))
            {
                return;
            }

            this.jobClient.Delete(JobIdList[eventSeatId]);
            JobIdList.TryRemove(eventSeatId, out _);
        }
    }
}
