// ****************************************************************************
// <copyright file="VenueServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class VenueServiceTests
    {
        private IVenueService venueService;

        private IRepository<Venue> venueRepository;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var venues = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };
            var fakeVenueRepository = new RepositoryFake<Venue>(venues);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeVenueRepository)
                .As<IRepository<Venue>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddVenue_WhenAddNewVenue_ShouldBeSaveNewVenueInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            this.venueRepository = this.Mock.Create<IRepository<Venue>>();
            this.venueService = this.Mock.Create<VenueService>();
            var dto = this.Fixture.Build<Venue>().Create();
            var expected = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
                dto,
            };

            // Act
            var expectedEntityId = this.venueService.AddVenue(dto);

            // Assert
            expected.Should().BeEquivalentTo(this.venueRepository.GetAll());
            dto.Id.Should().Be(expectedEntityId);
        }

        [Test]
        public void UpdateVenue_WhenUpdateVenueWithExistingVenueId_ShouldBeUpdateAllFieldsInTheRepository()
        {
            // Arrange
            this.venueRepository = this.Mock.Create<IRepository<Venue>>();
            this.venueService = this.Mock.Create<VenueService>();
            const int existingVenueId = 1;
            var expectedDto = this.Fixture.Build<Venue>().With(e => e.Id, existingVenueId).Create();

            // Act
            this.venueService.UpdateVenue(expectedDto);

            // Assert
            this.venueRepository.GetById(existingVenueId).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteVenue_WhenDeleteVenueWithExistingVenueId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            this.venueRepository = this.Mock.Create<IRepository<Venue>>();
            this.venueService = this.Mock.Create<VenueService>();
            const int existingVenueId = 1;
            var expected = new List<Venue>
            {
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };

            // Act
            this.venueService.DeleteVenue(existingVenueId);

            // Assert
            expected.Should().BeEquivalentTo(this.venueRepository.GetAll());
        }

        [Test]
        public void GetVenue_WhenGetVenueWithExistingVenueId_ShouldBeReturnThisVenue()
        {
            // Arrange
            this.venueService = this.Mock.Create<VenueService>();
            const int existingVenueId = 1;
            var expectedDto = new Venue() { Id = existingVenueId, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" };

            // Act
            var actualDto = this.venueService.GetVenue(existingVenueId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetVenues_WhenGetVenues_ShouldBeReturnAllVenues()
        {
            // Arrange
            this.venueService = this.Mock.Create<VenueService>();
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
