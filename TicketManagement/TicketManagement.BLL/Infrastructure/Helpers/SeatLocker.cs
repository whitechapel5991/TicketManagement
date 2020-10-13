// ****************************************************************************
// <copyright file="SeatLocker.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Infrastructure.Helpers
{
    public class SeatLocker : ISeatLocker
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;

        public SeatLocker(IRepository<Order> orderRepository, IRepository<EventSeat> eventSeatRepository)
        {
            this.orderRepository = orderRepository;
            this.eventSeatRepository = eventSeatRepository;
        }

        public void UnlockSeat(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            eventSeat.State = EventSeatState.Free;
            this.eventSeatRepository.Update(eventSeat);
            this.orderRepository.Delete(orderId);
        }
    }
}
