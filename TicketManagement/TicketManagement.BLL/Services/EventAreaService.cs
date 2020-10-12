// ****************************************************************************
// <copyright file="EventAreaService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Dto;
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

        // ?
        public EventAreaDto GetEventAreaMap(int eventAreaId)
        {
            var eventArea = this.eventAreaRepository.GetById(eventAreaId);

            var query = (from eventSeat in this.eventSeatRepository.GetAll().Where(x => x.EventAreaId == eventAreaId)
                         select new EventSeat
                         {
                             Id = eventSeat.Id,
                             Number = eventSeat.Number,
                             Row = eventSeat.Row,
                             State = eventSeat.State,
                             EventAreaId = eventAreaId,
                         }).ToList();

            var eventAreaDto = new EventAreaDto()
            {
                Id = eventArea.Id,
                CoordinateX = eventArea.CoordinateX,
                CoordinateY = eventArea.CoordinateY,
                Description = eventArea.Description,
                Price = eventArea.Price,
                Event = new EventDto { Id = eventArea.EventId },
            };

            var eventSeatDtoList = query.Select(eventSeat => new EventSeatDto()
                {
                    Id = eventSeat.Id,
                    Number = eventSeat.Number,
                    Row = eventSeat.Row,
                    State = eventSeat.State,
                    EventArea = eventAreaDto,
                })
                .ToList();

            eventAreaDto.EventSeats = eventSeatDtoList;

            return eventAreaDto;
        }
    }
}
