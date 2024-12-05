// ****************************************************************************
// <copyright file="IEventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    internal interface IEventService
    {
        IEnumerable<Event> GetEvents();

        Event GetEvent(int id);

        int AddEvent(Event entity);

        void UpdateEvent(Event entity);

        void DeleteEvent(int id);
    }
}
