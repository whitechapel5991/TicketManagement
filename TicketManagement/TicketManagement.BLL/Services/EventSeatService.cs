// ****************************************************************************
// <copyright file="EventSeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class EventSeatService : IEventSeatService
    {
        private readonly IRepository<EventSeat> eventSeatRepository;

        public EventSeatService(
            IRepository<EventSeat> eventSeatRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
        }

        public void UpdateEventSeat(EventSeat eventSeatDto)
        {
            var eventSeat = this.eventSeatRepository.GetById(eventSeatDto.Id);
            eventSeat.State = eventSeatDto.State;
            this.eventSeatRepository.Update(eventSeat);
        }

        public EventSeat GetEventSeat(int id)
        {
            return this.eventSeatRepository.GetById(id);
        }

        public IEnumerable<EventSeat> GetEventSeats()
        {
            return this.eventSeatRepository.GetAll();
        }

        public IEnumerable<EventSeat> GetEventSeatsByEventSeatIds(int[] idArray)
        {
            return this.eventSeatRepository.GetAll().Where(x => idArray.Contains(x.Id));
        }

        public IEnumerable<EventSeat> GetEventSeatsByEventAreaId(int eventAreaId)
        {
            return this.eventSeatRepository.GetAll().Where(x => x.EventAreaId == eventAreaId);
        }
    }
}
