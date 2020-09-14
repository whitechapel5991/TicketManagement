// ****************************************************************************
// <copyright file="EventAreaService.cs" company="EPAM Systems">
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
    public class EventAreaService : IEventAreaService
    {
        private readonly IRepository<EventArea> eventAreaRepository;

        public EventAreaService(IRepository<EventArea> eventAreaRepository)
        {
            this.eventAreaRepository = eventAreaRepository;
        }

        public void UpdateEventArea(EventArea eventAreaDto)
        {
            this.eventAreaRepository.Update(eventAreaDto);
        }

        public EventArea GetEventArea(int id)
        {
            return this.eventAreaRepository.GetById(id);
        }

        public IEnumerable<EventArea> GetEventAreas()
        {
            return this.eventAreaRepository.GetAll();
        }
    }
}
