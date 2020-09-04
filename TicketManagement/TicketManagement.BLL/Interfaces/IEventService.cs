// ****************************************************************************
// <copyright file="IEventService.cs" company="EPAM Systems">
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

namespace TicketManagement.BLL.Interfaces
{
    internal interface IEventService
    {
        IEnumerable<EventDto> GetEvents();

        EventDto GetEvent(int id);

        int AddEvent(EventDto entity);

        void UpdateEvent(EventDto entity);

        void DeleteEvent(int id);
    }
}
