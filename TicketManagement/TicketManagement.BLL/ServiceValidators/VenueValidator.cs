// ****************************************************************************
// <copyright file="VenueValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.BLL.Exceptions.VenueExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.ServiceValidators
{
    public class VenueValidator : IVenueValidator
    {
        private readonly IRepository<Venue, int> venueRepository;

        public VenueValidator(IRepository<Venue, int> venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public void Validate(Venue entity)
        {
            if (this.VenueNameExist(entity.Name))
            {
                throw new VenueWithThisNameExistException($"Venue with name:{entity.Name} already exist.");
            }
        }

        private bool VenueNameExist(string nameVenue)
        {
            return this.venueRepository.GetAll().Where(x => x.Name == nameVenue).Any();
        }
    }
}
