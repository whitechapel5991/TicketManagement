// ****************************************************************************
// <copyright file="OrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.BLL.Infrastructure.Helpers.Jobs;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Layout> layoutRepository;
        private readonly UserManager<TicketManagementUser, int> userManager;
        private readonly IEmailHelper emailHelper;
        private readonly IHtmlHelper htmlHelper;
        private readonly IOrderValidator orderValidator;
        private readonly ISeatUnlockScheduler seatUnlockScheduler;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<EventSeat> eventSeatRepository,
            UserManager<TicketManagementUser, int> userManager,
            IEmailHelper emailHelper,
            IHtmlHelper htmlHelper,
            IRepository<EventArea> eventAreaRepository,
            IRepository<Event> eventRepository,
            IRepository<Layout> layoutRepository,
            IOrderValidator orderValidator,
            ISeatUnlockScheduler seatUnlockScheduler)
        {
            this.orderRepository = orderRepository;
            this.eventSeatRepository = eventSeatRepository;
            this.userManager = userManager;
            this.emailHelper = emailHelper;
            this.htmlHelper = htmlHelper;
            this.eventAreaRepository = eventAreaRepository;
            this.eventRepository = eventRepository;
            this.layoutRepository = layoutRepository;
            this.orderValidator = orderValidator;
            this.seatUnlockScheduler = seatUnlockScheduler;
        }

        public void AddToCart(int eventSeatId, int userId)
        {
            Order newOrder = new Order()
            {
                Date = DateTime.Now,
                EventSeatId = eventSeatId,
                UserId = userId,
            };

            EventSeat eventSeat = this.eventSeatRepository.GetById(eventSeatId);
            this.orderValidator.SeatIsBlocked(eventSeat);
            eventSeat.State = EventSeatState.InBasket;
            this.eventSeatRepository.Update(eventSeat);

            this.orderRepository.Create(newOrder);
            this.seatUnlockScheduler.Start(eventSeatId);
        }

        public void Buy(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            eventSeat.State = EventSeatState.Sold;
            order.Date = DateTime.Now;
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Update(order);

            // Send letter
            var eventArea = this.eventAreaRepository.GetById(eventSeat.EventAreaId);
            var @event = this.eventRepository.GetById(eventArea.EventId);
            var layout = this.layoutRepository.GetById(@event.LayoutId);
            string eventHtml = this.htmlHelper.GetEventHtml(eventSeat, eventArea, @event, layout);
            var userEmail = this.userManager.FindById(order.UserId).Email;
            this.emailHelper.SendEmail(userEmail, @event.Name, eventHtml);
        }

        public void DeleteFromCart(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            eventSeat.State = EventSeatState.Free;
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Delete(orderId);
            this.seatUnlockScheduler.Shutdown(order.EventSeatId);
        }

        public List<Order> GetCartOrdersById(int userId)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.InBasket) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }

        public List<Order> GetCartOrdersByName(string userName)
        {
            var userId = this.userManager.FindByName(userName).Id;
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
            var userId = this.userManager.FindByName(userName).Id;
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == userId).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }
    }
}
