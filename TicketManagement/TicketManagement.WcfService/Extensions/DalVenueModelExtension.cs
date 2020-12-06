// ****************************************************************************
// <copyright file="DalVenueModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalVenueModelExtension
    {
        public static Venue ConvertToWcfVenue(this TicketManagement.DAL.Models.Venue bllVenue)
        {
            return new Venue()
            {
                Id = bllVenue.Id,
                Name = bllVenue.Name,
                Description = bllVenue.Description,
                Address = bllVenue.Address,
                Phone = bllVenue.Phone,
            };
        }
    }
}