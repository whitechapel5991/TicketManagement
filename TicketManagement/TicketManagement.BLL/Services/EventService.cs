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
        private readonly IRepository<Event, int> eventRepository;
        private readonly IRepository<Layout, int> layoutRepository;
        private readonly IRepository<EventArea, int> eventAreaRepository;
        private readonly IRepository<EventSeat, int> eventSeatRepository;

        private readonly IEventValidator eventValidator;

        public EventService(
            IRepository<Event, int> eventRepository,
            IRepository<Layout, int> layoutRepository,
            IRepository<EventArea, int> eventAreaRepository,
            IRepository<EventSeat, int> eventSeatRepository,
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
            this.eventValidator.Validate(eventDto);
            this.eventValidator.UpdateValidate(eventDto);

            this.eventRepository.Update(eventDto);
        }

        public int AddEvent(Event eventDto)
        {
            this.eventValidator.Validate(eventDto);
            return this.eventRepository.Create(eventDto);
        }

        public void DeleteEvent(int id)
        {
            this.eventValidator.DeleteValidate(id);
            this.eventRepository.Delete(id);
        }

        public void PublishEvent(int id)
        {
            var @event = this.eventRepository.GetById(id);
            this.eventValidator.PublishValidate(@event);

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

        public int AvailibleSeatCount(int eventId)
        {
            var eventAreaIdList = this.eventAreaRepository.GetAll().Where(x => x.EventId == eventId).Select(x => x.Id);
            var seats = this.eventSeatRepository.GetAll().Where(e => eventAreaIdList.Contains(e.EventAreaId)).Where(x => x.State == EventSeatState.Free);
            var countFreeSeats = seats.Count();
            return countFreeSeats;
        }

        public EventDto GetEventMap(int eventId)
        {
            var @event = this.eventRepository.GetById(eventId);
            var layout = this.layoutRepository.GetById(@event.LayoutId);

            var eventDto = this.MapToEventDto(@event, layout);

            var query = (from eventArea in this.eventAreaRepository.GetAll().Where(x => x.EventId == @event.Id)
                         join eventSeat in this.eventSeatRepository.GetAll() on eventArea.Id equals eventSeat.EventAreaId into eventSeatJoin
                         select new
                         {
                             eventArea.Id,
                             eventArea.CoordX,
                             eventArea.CoordY,
                             eventArea.Description,
                             eventArea.Price,
                             EventSeats = eventSeatJoin,
                         }).ToList();

            var eventAreaDtoList = new List<EventAreaDto>();
            foreach (var eventArea in query)
            {
                EventAreaDto eventAreaDto = new EventAreaDto()
                {
                    Id = eventArea.Id,
                    CoordX = eventArea.CoordX,
                    CoordY = eventArea.CoordY,
                    Description = eventArea.Description,
                    Price = eventArea.Price,
                    Event = eventDto,
                };

                var eventSeatDtoList = new List<EventSeatDto>();

                foreach (var eventSeat in eventArea.EventSeats)
                {
                    EventSeatDto eventSeatDto = new EventSeatDto()
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

        private EventDto MapToEventDto(Event @event, Layout layout)
        {
            return new EventDto()
            {
                Id = @event.Id,
                Description = @event.Description,
                BeginDate = @event.BeginDate,
                EndDate = @event.EndDate,
                Name = @event.Name,
                Published = @event.Published,
                Layout = layout,
            };
        }
    }
}
