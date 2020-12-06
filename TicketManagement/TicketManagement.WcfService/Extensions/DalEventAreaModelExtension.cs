// ****************************************************************************
// <copyright file="DalEventAreaModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalEventAreaModelExtension
    {
        public static EventArea ConvertToWcfEventArea(this TicketManagement.DAL.Models.EventArea bllEventArea)
        {
            return new EventArea()
            {
                Id = bllEventArea.Id,
                Description = bllEventArea.Description,
                CoordinateX = bllEventArea.CoordinateX,
                CoordinateY = bllEventArea.CoordinateY,
                Price = bllEventArea.Price,
                EventId = bllEventArea.EventId,
            };
        }
    }
}