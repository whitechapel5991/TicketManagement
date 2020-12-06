// ****************************************************************************
// <copyright file="DalEventModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalEventModelExtension
    {
        public static Event ConvertToWcfEvent(this TicketManagement.DAL.Models.Event bllEvent)
        {
            return new Event()
            {
                Id = bllEvent.Id,
                Name = bllEvent.Name,
                BeginDateUtc = bllEvent.BeginDateUtc,
                EndDateUtc = bllEvent.EndDateUtc,
                Description = bllEvent.Description,
                Published = bllEvent.Published,
                LayoutId = bllEvent.LayoutId,
                ImageUrl = bllEvent.ImageUrl,
            };
        }
    }
}