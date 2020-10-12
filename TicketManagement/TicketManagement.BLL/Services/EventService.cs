// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Dto;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Layout> layoutRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IEventValidator eventValidator;

        public EventService(
            IRepository<Event> eventRepository,
            IRepository<Layout> layoutRepository,
            IRepository<EventArea> eventAreaRepository,
            IRepository<EventSeat> eventSeatRepository,
            IEventValidator eventValidator)
        {
            this.eventRepository = eventRepository;
            this.layoutRepository = layoutRepository;
            this.eventAreaRepository = eventAreaRepository;
            this.eventSeatRepository = eventSeatRepository;
            this.eventValidator = eventValidator;
        }

        public void UpdateEvent(Event eventDto)
        {
            this.eventValidator.UpdateValidation(eventDto);
            this.eventRepository.Update(eventDto);
        }

        public int AddEvent(Event eventDto)
        {
            this.eventValidator.Validation(eventDto);
            return this.eventRepository.Create(eventDto);
        }

        public void DeleteEvent(int id)
        {
            this.eventValidator.DeleteValidation(id);
            this.eventRepository.Delete(id);
        }

        public void PublishEvent(int id)
        {
            var @event = this.eventRepository.GetById(id);
            this.eventValidator.PublishValidation(@event);

            @event.Published = true;

            this.eventRepository.Update(@event);
        }

        public Event GetEvent(int id)
        {
            return this.eventRepository.GetById(id);
        }

        public IEnumerable<Event> GetEvents()
        {
            return this.eventRepository.GetAll();
        }

        public IEnumerable<Event> GetPublishEvents()
        {
            return this.eventRepository.GetAll().Where(x => x.Published == true);
        }

        public int GetAvailableSeatCount(int eventId)
        {
            var eventAreaIdList = this.eventAreaRepository.GetAll()
                .Where(x => x.EventId == eventId)
                .Select(x => x.Id);
            return this.eventSeatRepository.GetAll()
                .Where(e => eventAreaIdList.Contains(e.EventAreaId))
                .Count(x => x.State == EventSeatState.Free);
        }

        public Event GetEventByEventSeatId(int eventSeatId)
        {
            var eventAreaId = this.eventSeatRepository.GetById(eventSeatId).EventAreaId;
            var eventId = this.eventAreaRepository.GetById(eventAreaId).EventId;

            return this.eventRepository.GetById(eventId);
        }

        // ?
        public EventDto GetEventMap(int eventId)
        {
            var eventDetails = this.eventRepository.GetById(eventId);
            var layout = this.layoutRepository.GetById(eventDetails.LayoutId);

            var eventDto = this.MapToEventDto(eventDetails, layout);

            var query = (from eventArea in this.eventAreaRepository.GetAll().Where(x => x.EventId == eventDetails.Id)
                         join eventSeat in this.eventSeatRepository.GetAll() on eventArea.Id equals eventSeat.EventAreaId into eventSeatJoin
                         select new
                         {
                             eventArea.Id,
                             CoordX = eventArea.CoordinateX,
                             CoordY = eventArea.CoordinateY,
                             eventArea.Description,
                             eventArea.Price,
                             EventSeats = eventSeatJoin,
                         }).ToList();

            var eventAreaDtoList = new List<EventAreaDto>();
            foreach (var eventArea in query)
            {
                var eventAreaDto = new EventAreaDto()
                {
                    Id = eventArea.Id,
                    CoordinateX = eventArea.CoordX,
                    CoordinateY = eventArea.CoordY,
                    Description = eventArea.Description,
                    Price = eventArea.Price,
                    Event = eventDto,
                };

                var eventSeatDtoList = new List<EventSeatDto>();

                foreach (var eventSeat in eventArea.EventSeats)
                {
                    var eventSeatDto = new EventSeatDto()
                    {
                        Id = eventSeat.Id,
                        Number = eventSeat.Number,
                        Row = eventSeat.Row,
                        State = eventSeat.State,
                        EventArea = eventAreaDto,
                    };

                    eventSeatDtoList.Add(eventSeatDto);
                }

                eventAreaDto.EventSeats = eventSeatDtoList;
                eventAreaDtoList.Add(eventAreaDto);
            }

            eventDto.EventAreas = eventAreaDtoList;

            return eventDto;
        }

        private EventDto MapToEventDto(Event eventDetail, Layout layout)
        {
            return new EventDto()
            {
                Id = eventDetail.Id,
                Description = eventDetail.Description,
                BeginDate = eventDetail.BeginDate,
                EndDate = eventDetail.EndDate,
                Name = eventDetail.Name,
                Published = eventDetail.Published,
                Layout = layout,
            };
        }
    }
}
