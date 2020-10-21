// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IEventValidator eventValidator;

        public EventService(
            IRepository<Event> eventRepository,
            IRepository<EventArea> eventAreaRepository,
            IRepository<EventSeat> eventSeatRepository,
            IEventValidator eventValidator)
        {
            this.eventRepository = eventRepository;
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
            return this.eventRepository.GetAll().Where(x => x.Published);
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

        public IEnumerable<Event> GetEventsByEventSeatIds(int[] eventSeatIdArray)
        {
            var eventAreaIdCollection = this.eventSeatRepository.GetAll().Where(x => eventSeatIdArray.Contains(x.Id)).Select(x => x.EventAreaId);
            var eventIdCollection = this.eventAreaRepository.GetAll().Where(x => eventAreaIdCollection.Contains(x.Id)).Select(x => x.EventId);

            return this.eventRepository.GetAll().Where(x => eventIdCollection.Contains(x.Id));
        }
    }
}
