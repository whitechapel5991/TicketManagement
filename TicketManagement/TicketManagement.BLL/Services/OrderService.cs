// ****************************************************************************
// <copyright file="OrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
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

        public OrderService(IRepository<Order> orderRepository, IRepository<EventSeat> eventSeatRepository)
        {
            this.orderRepository = orderRepository;
            this.eventSeatRepository = eventSeatRepository;
        }

        public void AddToCart(int eventSeatId, TicketManagementUser user)
        {
            Order newOrder = new Order()
            {
                Date = DateTime.Now,
                EventSeatId = eventSeatId,
                UserId = user.Id,
            };

            EventSeat eventSeat = this.eventSeatRepository.GetById(eventSeatId);
            eventSeat.State = EventSeatState.InBasket;
            this.eventSeatRepository.Update(eventSeat);

            this.orderRepository.Create(newOrder);
        }

        public List<Order> GetHistoryOrdersById(TicketManagementUser user)
        {
            return (from userOrderQ in this.orderRepository.GetAll().Where(x => x.UserId == user.Id).ToArray()
                    join eventSeatQ in this.eventSeatRepository.GetAll().Where(x => x.State == EventSeatState.Sold) on userOrderQ.EventSeatId equals eventSeatQ.Id
                    select new Order { Id = userOrderQ.Id, Date = userOrderQ.Date, EventSeatId = userOrderQ.EventSeatId, UserId = userOrderQ.UserId }).ToList();
        }
    }
}
