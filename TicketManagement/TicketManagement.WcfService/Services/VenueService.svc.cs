// ****************************************************************************
// <copyright file="VenueService.svc.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.WcfService.Contracts;
using TicketManagement.WcfService.Extensions;

namespace TicketManagement.WcfService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VenueService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VenueService.svc or VenueService.svc.cs at the Solution Explorer and start debugging.
    public class VenueService : IVenueContract
    {
        private readonly IVenueService venueService;

        public VenueService(IVenueService venueService)
        {
            this.venueService = venueService;
        }

        public int AddVenue(Venue entity)
        {
            return this.venueService.AddVenue(entity.ConvertToBllVenue());
        }

        public void DeleteVenue(int id)
        {
            this.venueService.DeleteVenue(id);
        }

        public Venue GetVenue(int id)
        {
            return this.venueService.GetVenue(id).ConvertToWcfVenue();
        }

        public IEnumerable<Venue> GetVenues()
        {
            return this.venueService.GetVenues().Select(entity => entity.ConvertToWcfVenue());
        }

        public void UpdateVenue(Venue entity)
        {
            this.venueService.UpdateVenue(entity.ConvertToBllVenue());
        }
    }
}
