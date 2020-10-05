// ****************************************************************************
// <copyright file="IHtmlHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface IHtmlHelper
    {
        string GetEventHtml(EventSeat eventSeat, EventArea eventArea, Event @event, Layout layout);
    }
}
