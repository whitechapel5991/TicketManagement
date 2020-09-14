// ****************************************************************************
// <copyright file="EventSeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class EventSeatService : IEventSeatService
    {
        private readonly IRepository<EventSeat> eventSeatRepository;

        public EventSeatService(IRepository<EventSeat> eventSeatRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
        }

        public void UpdateEventSeat(EventSeat eventSeatDto)
        {
            this.eventSeatRepository.Update(eventSeatDto);
        }

        public EventSeat GetEventSeat(int id)
        {
            return this.eventSeatRepository.GetById(id);
        }

        public IEnumerable<EventSeat> GetEventSeats()
        {
            return this.eventSeatRepository.GetAll();
        }
    }
}
