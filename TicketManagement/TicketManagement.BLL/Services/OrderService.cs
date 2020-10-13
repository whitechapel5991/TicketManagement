// ****************************************************************************
// <copyright file="OrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
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
        private static readonly Dictionary<int, string> JobIdList;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Layout> layoutRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailHelper emailHelper;
        private readonly IOrderValidator orderValidator;
        private readonly IBackgroundJobClient jobClient;
        private readonly int seatLockTimeMinutes;

        static OrderService()
        {
            JobIdList = new Dictionary<int, string>();
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
        }

        public void AddToCart(int eventSeatId, int userId)
        {
            var newOrder = new Order()
            {
                Date = DateTime.Now,
                EventSeatId = eventSeatId,
                UserId = userId,
            };

            var eventSeat = this.eventSeatRepository.GetById(eventSeatId);
            this.orderValidator.SeatIsBlocked(eventSeat);
            eventSeat.State = EventSeatState.InBasket;
            this.eventSeatRepository.Update(eventSeat);

            var orderId = this.orderRepository.Create(newOrder);

            var jobId = this.jobClient.Schedule<ISeatLocker>(
                x =>
                    x.UnlockSeat(orderId),
                TimeSpan.FromMinutes(this.seatLockTimeMinutes));

            JobIdList.Add(eventSeatId, jobId);
        }

        public void Buy(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            this.jobClient.Delete(JobIdList[eventSeat.Id]);
            JobIdList.Remove(eventSeat.Id);
            eventSeat.State = EventSeatState.Sold;
            order.Date = DateTime.Now;
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Update(order);

            // Send letter
            var eventArea = this.eventAreaRepository.GetById(eventSeat.EventAreaId);
            var @event = this.eventRepository.GetById(eventArea.EventId);
            var layout = this.layoutRepository.GetById(@event.LayoutId);
            var eventHtml = this.GetEventHtml(eventSeat, eventArea, @event, layout);
            var userEmail = this.userRepository.GetById(order.UserId).Email;
            this.emailHelper.SendEmail(userEmail, @event.Name, eventHtml);
        }

        public void DeleteFromCart(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            eventSeat.State = EventSeatState.Free;
            this.jobClient.Delete(JobIdList[eventSeat.Id]);
            JobIdList.Remove(eventSeat.Id);
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Delete(orderId);
        }

        public List<Order> GetCartOrdersById(int userId)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.InBasket) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }

        public List<Order> GetCartOrdersByName(string userName)
        {
            var userId = this.userRepository.FindByNormalizedUserName(userName).Id;
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.InBasket) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }

        public List<Order> GetHistoryOrdersById(int userId)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }

        public List<Order> GetHistoryOrdersByName(string userName)
        {
            var userId = this.userRepository.FindByNormalizedUserName(userName).Id;
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }

        private string GetEventHtml(EventSeat eventSeat, EventArea eventArea, Event @event, Layout layout)
        {
            const string htmlTableStart = "<table style=\"background: #ffffff; border-radius: 3px; width: 100%;\" > " +
                                          "<tr> " +
                                          "<td style=\"box-sizing: border-box; padding: 20px;\"> " +
                                          "<table style=\"border=\"0\" cellpadding=\"0\" cellspacing=\"0\";\" > " +
                                          "<tr>" +
                                          "<td>";

            var content = $"<p>Dear client,</p>" +
                          $"Congratulations on your purchase for the {@event.Name} which started {@event.BeginDate} and ended {@event.EndDate}." +
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
    }
}
