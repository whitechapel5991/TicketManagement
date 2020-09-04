// ****************************************************************************
// <copyright file="EventAreaService.cs" company="EPAM Systems">
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
    public class EventAreaService : IEventAreaService
    {
        public EventAreaDto GetEventArea(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventAreaDto> GetEventAreas()
        {
            throw new NotImplementedException();
        }

        public void UpdateEventArea(EventAreaDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
