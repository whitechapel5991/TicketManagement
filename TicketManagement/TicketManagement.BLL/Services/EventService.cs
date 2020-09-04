// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.DTO;
using TicketManagement.BLL.Interfaces;

namespace TicketManagement.BLL.Services
{
    public class EventService : IEventService
    {
        public int AddEvent(EventDto entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public EventDto GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDto> GetEvents()
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(EventDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
