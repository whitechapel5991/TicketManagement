// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepository;

        private readonly IEventValidator eventValidator;

        public EventService(
            IRepository<Event> eventRepository,
            IEventValidator eventValidator)
        {
            this.eventRepository = eventRepository;
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

        public Event GetEvent(int id)
        {
            return this.eventRepository.GetById(id);
        }

        public IEnumerable<Event> GetEvents()
        {
            return this.eventRepository.GetAll();
        }
    }
}
