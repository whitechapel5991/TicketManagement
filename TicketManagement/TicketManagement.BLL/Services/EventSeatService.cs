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
        private readonly IRepository<EventSeat, int> eventSeatRepository;
        private readonly IRepository<EventArea, int> eventAreaRepository;
        private readonly IRepository<Event, int> eventRepository;

        public EventSeatService(
            IRepository<EventSeat, int> eventSeatRepository,
            IRepository<EventArea, int> eventAreaRepository,
            IRepository<Event, int> eventRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
            this.eventAreaRepository = eventAreaRepository;
            this.eventRepository = eventRepository;
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

        public decimal GetSeatCost(int seatId)
        {
            var eventAreaId = this.eventSeatRepository.GetById(seatId).EventAreaId;
            return this.eventAreaRepository.GetById(eventAreaId).Price;
        }

        public Event GetEventByEventSeatId(int seatId)
        {
            var eventAreaId = this.eventSeatRepository.GetById(seatId).EventAreaId;
            var eventId = this.eventAreaRepository.GetById(eventAreaId).EventId;

            return this.eventRepository.GetById(eventId);
        }
    }
}
