// ****************************************************************************
// <copyright file="DalLayoutModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalLayoutModelExtension
    {
        public static Layout ConvertToWcfLayout(this TicketManagement.DAL.Models.Layout bllLayout)
        {
            return new Layout()
            {
                Id = bllLayout.Id,
                Name = bllLayout.Name,
                Description = bllLayout.Description,
                VenueId = bllLayout.VenueId,
            };
        }
    }
}