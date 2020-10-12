// ****************************************************************************
// <copyright file="VenueServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class VenueServiceTests : Test
    {
        private IVenueService venueService;
        private IRepository<Venue> venueRepository;

        [SetUp]
        public void Init()
        {
            this.venueService = this.Container.Resolve<IVenueService>();
            this.venueRepository = this.Container.Resolve<IRepository<Venue>>();
        }

        [Test]
        public void AddVenue_WhenAddNewVenue_ShouldBeSaveNewVenueInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            var venueDto = new Venue
            {
                Description = "3",
                Name = "3",
                Address = "3",
                Phone = "3",
            };
            var expected = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
                venueDto,
            };

            // Act
            this.venueService.AddVenue(venueDto);

            // Assert
            this.venueRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateVenue_WhenUpdateVenueWithExistingVenueId_ShouldBeUpdateAllFieldsInTheRepository()
        {
            // Arrange
            var venueDto = new Venue
            {
                Id = 2,
                Description = "3",
                Name = "3",
                Address = "3",
                Phone = "3",
            };

            // Act
            this.venueService.UpdateVenue(venueDto);

            // Assert
            this.venueRepository.GetById(venueDto.Id).Should().BeEquivalentTo(venueDto);
        }

        [Test]
        public void DeleteVenue_WhenDeleteVenueWithExistingVenueId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            const int existingVenueId = 1;
            var expected = new List<Venue>
            {
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };

            // Act
            this.venueService.DeleteVenue(existingVenueId);

            // Assert
            this.venueRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetVenue_WhenGetVenueWithExistingVenueId_ShouldBeReturnThisVenue()
        {
            // Arrange
            const int existingVenueId = 1;
            var expectedDto = new Venue() { Id = existingVenueId, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" };

            // Act
            var actualDto = this.venueService.GetVenue(existingVenueId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetVenues_GetVenuesCount()
        {
            // Arrange
            var expected = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };

            // Act
            var actual = this.venueService.GetVenues();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
