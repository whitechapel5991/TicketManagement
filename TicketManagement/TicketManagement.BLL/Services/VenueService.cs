// ****************************************************************************
// <copyright file="VenueService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BLL.DTO;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Venue> venueRepository;
        private readonly IMapper mapper;

        public VenueService(IRepository<Venue> venueRepository, IMapper mapper)
        {
            this.venueRepository = venueRepository;
            this.mapper = mapper;
        }

        public int AddVenue(VenueDto entity)
        {
            if (this.IsVenueName(entity.Name))
            {
                throw new TicketManagementException("there is venue with this name", "VenueWithThatNameException");
            }

            Venue venue = this.mapper.Map<Venue>(entity);

            return Convert.ToInt32(this.venueRepository.Create(venue));
        }

        public void DeleteVenue(int id)
        {
            if (id != null)
            {
                this.venueRepository.Delete(id);
            }
            else
            {
                throw new TicketManagementException("not set the id venue", string.Empty);
            }
        }

        public VenueDto GetVenue(int id)
        {
            Venue venue;
            try
            {
                venue = this.venueRepository.GetById(id);
            }
            catch (InvalidOperationException)
            {
                throw new TicketManagementException("venue not found", "VenueNotFound");
            }

            return this.mapper.Map<Venue, VenueDto>(venue);
        }

        public IEnumerable<VenueDto> GetVenues()
        {
            return this.mapper.Map<IEnumerable<Venue>, IEnumerable<VenueDto>>(this.venueRepository.GetAll());
        }

        public void UpdateVenue(VenueDto entity)
        {
            Venue venue;
            try
            {
                venue = this.venueRepository.GetById(entity.Id);
            }
            catch (InvalidOperationException)
            {
                throw new TicketManagementException("venue not found", "VenueNotFound");
            }

            if (this.IsVenueName(entity.Name))
            {
                throw new TicketManagementException("there is venue with this name", "VenueWithThatNameException");
            }

            venue = this.mapper.Map<Venue>(entity);

            this.venueRepository.Update(venue);
        }

        private bool IsVenueName(string nameVenue)
        {
            var venueQuery = this.venueRepository.GetAll().Where(x => x.Name == nameVenue);
            return venueQuery.Any();
        }
    }
}
