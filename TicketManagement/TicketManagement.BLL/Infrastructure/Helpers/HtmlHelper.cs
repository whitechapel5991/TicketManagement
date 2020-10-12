// ****************************************************************************
// <copyright file="HtmlHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Infrastructure.Helpers
{
    public class HtmlHelper : IHtmlHelper
    {
        public string GetEventHtml(EventSeat eventSeat, EventArea eventArea, Event @event, Layout layout)
        {
            var htmlBody = string.Empty;
            var htmlTableStart = "<table style=\"background: #ffffff; border-radius: 3px; width: 100%;\" > " +
                                 "<tr> " +
                                 "<td style=\"box-sizing: border-box; padding: 20px;\"> " +
                                 "<table style=\"border=\"0\" cellpadding=\"0\" cellspacing=\"0\";\" > " +
                                 "<tr>" +
                                 "<td>";

            var content = $"<p>Dear client,</p>" +
                          $"Congratulations on your purchase for the {@event.Name} which started {@event.BeginDate} and ended {@event.EndDate}." +
                          $"Event description: {@event.Description}" +
                          $"Layout of the event is {layout.Name}. Description: {layout.Description}" +
                          $"Area of the event is {eventArea.Description} which is located by coordinates X: {eventArea.CoordinateX}, Y: {eventArea.CoordinateY}" +
                          $"Cost of event is {eventArea.Price}" +
                          $"Your seat is in {eventSeat.Row} row and {eventSeat.Number} number." +
                          $"<p>Good luck!</p>";

            var htmlTableEnd = "</td> " +
                               "</tr> " +
                               "</table> " +
                               "</td> " +
                               "</tr> " +
                               "</table>";

            htmlBody += htmlTableStart;
            htmlBody += content;
            htmlBody += htmlTableEnd;

            return htmlBody;
        }
    }
}
