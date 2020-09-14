// ****************************************************************************
// <copyright file="IEventAreaService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    internal interface IEventAreaService
    {
        IEnumerable<EventArea> GetEventAreas();

        EventArea GetEventArea(int id);

        void UpdateEventArea(EventArea entity);
    }
}
