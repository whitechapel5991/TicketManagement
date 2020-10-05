// ****************************************************************************
// <copyright file="LayoutValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.LayoutExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.ServiceValidators
{
    internal class LayoutValidator : ILayoutValidator
    {
        private readonly IRepository<Layout, int> layoutRepository;
        private readonly IRepository<Venue, int> venueRepository;

        public LayoutValidator(
            IRepository<Layout, int> layoutRepository,
            IRepository<Venue, int> venueRepository)
        {
            this.layoutRepository = layoutRepository;
            this.venueRepository = venueRepository;
        }

        public void Validate(Layout entity)
        {
            var venue = this.venueRepository.GetById(entity.VenueId);
            if (venue == default(Venue))
            {
                throw new EntityDoesNotExistException($"Venue with id={entity.VenueId} doesn't exist.");
            }

            if (this.LayoutNameExist(entity.Name, entity.VenueId))
            {
                throw new LayoutWithSameNameInTheVenueExistException($"Layout with name={entity.Name} exist in the venue with id={entity.VenueId}");
            }
        }

        private bool LayoutNameExist(string nameLayout, int venueId)
        {
            return this.layoutRepository.GetAll()
                .Where(x => x.Name == nameLayout && x.VenueId == venueId).Any();
        }
    }
}
