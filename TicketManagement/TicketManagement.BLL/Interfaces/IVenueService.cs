// ****************************************************************************
// <copyright file="IVenueService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    public interface IVenueService
    {
        IEnumerable<Venue> GetVenues();

        Venue GetVenue(int id);

        int AddVenue(Venue entity);

        void UpdateVenue(Venue entity);

        void DeleteVenue(int id);
    }
}
