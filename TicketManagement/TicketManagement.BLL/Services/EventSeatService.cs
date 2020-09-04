// ****************************************************************************
// <copyright file="EventSeatService.cs" company="EPAM Systems">
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
    public class EventSeatService : IEventSeatService
    {
        public EventSeatDto GetEventSeat(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventSeatDto> GetEventSeats()
        {
            throw new NotImplementedException();
        }

        public void UpdateEventSeat(EventSeatDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
