// ****************************************************************************
// <copyright file="EventAreaService.cs" company="EPAM Systems">
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
    public class EventAreaService : IEventAreaService
    {
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;

        public EventAreaService(
            IRepository<EventArea> eventAreaRepository,
            IRepository<EventSeat> eventSeatRepository)
        {
            this.eventAreaRepository = eventAreaRepository;
            this.eventSeatRepository = eventSeatRepository;
        }

        public void UpdateEventArea(EventArea eventAreaDto)
        {
            var eventArea = this.eventAreaRepository.GetById(eventAreaDto.Id);
            eventArea.Price = eventAreaDto.Price;
            this.eventAreaRepository.Update(eventArea);
        }

        public EventArea GetEventArea(int id)
        {
            return this.eventAreaRepository.GetById(id);
        }

        public IEnumerable<EventArea> GetEventAreas()
        {
            return this.eventAreaRepository.GetAll();
        }

        public decimal GetEventAreaCost(int seatId)
        {
            var eventAreaId = this.eventSeatRepository.GetById(seatId).EventAreaId;
            return this.eventAreaRepository.GetById(eventAreaId).Price;
        }

        public IEnumerable<EventArea> GetEventAreasByEventSeatIds(int[] eventSeatIdArray)
        {
            var eventAreaIdArray = this.eventSeatRepository.GetAll()
                .Where(x => eventSeatIdArray.Contains(x.Id))
                .Select(x => x.EventAreaId);

            return this.eventAreaRepository.GetAll().Where(x => eventAreaIdArray.Contains(x.Id));
        }

        public IEnumerable<EventArea> GetEventAreasByEventId(int eventId)
        {
            return this.eventAreaRepository.GetAll().Where(x => x.EventId == eventId);
        }
    }
}
