// ****************************************************************************
// <copyright file="VenueService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    internal class VenueService : IVenueService
    {
        private readonly IRepository<Venue> venueRepository;
        private readonly IVenueValidator venueValidator;

        public VenueService(IRepository<Venue> venueRepository, IVenueValidator validator)
        {
            this.venueRepository = venueRepository;
            this.venueValidator = validator;
        }

        public int AddVenue(Venue entity)
        {
            this.venueValidator.Validate(entity);
            return this.venueRepository.Create(entity);
        }

        public void DeleteVenue(int id)
        {
            this.venueRepository.Delete(id);
        }

        public Venue GetVenue(int id)
        {
            return this.venueRepository.GetById(id);
        }

        public IEnumerable<Venue> GetVenues()
        {
            return this.venueRepository.GetAll();
        }

        public void UpdateVenue(Venue entity)
        {
            this.venueValidator.Validate(entity);
            this.venueRepository.Update(entity);
        }
    }
}
