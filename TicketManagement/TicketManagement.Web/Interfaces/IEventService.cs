// ****************************************************************************
// <copyright file="IEventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.Web.Models.Event;

namespace TicketManagement.Web.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventViewModel> GetPublishEvents();

        int AddToCart(int seatId, int userId);

        EventDetailViewModel GetEventDetailViewModel(int eventId);

        EventAreaDetailViewModel GetEventAreaDetailViewModel(int eventAreaId);
    }
}
