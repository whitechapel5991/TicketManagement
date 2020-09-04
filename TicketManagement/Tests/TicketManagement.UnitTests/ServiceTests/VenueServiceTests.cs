// ****************************************************************************
// <copyright file="VenueServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using NUnit.Framework;
using TicketManagement.BLL.DTO;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.ServiceTests.Base;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class VenueServiceTests : TestBase
    {
        private IVenueService venueService;
        private Fixture fixture;

        [OneTimeSetUp]
        public void Init()
        {
            this.fixture = new Fixture();

            var repositoryMoq = this.Mock.Mock<IRepository<Venue>>();

            repositoryMoq.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int id) => this.fixture.Build<Venue>().With(e => e.Id, id).Create())
                .Verifiable();

            repositoryMoq.Setup(x => x.Update(It.IsAny<Venue>())).Verifiable();
            repositoryMoq.Setup(x => x.Create(It.IsAny<Venue>()))
                .Returns((Venue entity) => It.Is<object>(x => Convert.ToInt32(x) == 100)).Verifiable();
            repositoryMoq.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();
            repositoryMoq.Setup(x => x.GetAll())
                .Returns(() =>
                {
                    return new List<Venue>()
                    {
                        this.fixture.Build<Venue>().With(e => e.Id, 1).Create(),
                        this.fixture.Build<Venue>().With(e => e.Id, 2).Create(),
                        this.fixture.Build<Venue>().With(e => e.Id, 3).Create(),
                        this.fixture.Build<Venue>().With(e => e.Id, 4).Create(),
                    };
                })
                .Verifiable();

            this.container.RegisterMock<IRepository<Venue>>(repositoryMoq);
            var container = this.container.Build();

            this.venueService = container.Resolve<IVenueService>();
        }

        [Test]
        public void AddVenue_AddNewVenue_GetCountVenues()
        {
            // Arrange
            var venueDto = new VenueDto
            {
                Id = 9,
                Description = "№9",
                Name = "9",
                Address = "9",
                Phone = "9",
            };

            // Act
            this.venueService.AddVenue(venueDto);

            // Assert
            Assert.AreEqual(3, this.venueService.GetVenues().Count());
        }

        [Test]
        public void UpdateVenue_NewVenue_GetVenueDescription()
        {
            // Arrange
            var venueDto = this.fixture.Build<VenueDto>().With(e => e.Id, 1).Create();

            // Act
            this.venueService.UpdateVenue(venueDto);

            // Assert
            this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Update(It.IsAny<Venue>()), Times.Once);

            // Assert.AreEqual("№9", this.venueService.GetVenue(1).Description);
        }

        [Test]
        public void DeleteVenue_VenueId_GetVenuesCount()
        {
            // Arrange

            // Act
            this.venueService.DeleteVenue(1);

            // Assert
            Assert.AreEqual(1, this.venueService.GetVenues().Count());
        }

        [Test]
        public void GetVenue_VenueId_GetVenueDescription()
        {
            // Arrange

            // Act
            var venue = this.venueService.GetVenue(1);

            // Assert
            Assert.AreEqual("First venue", venue.Description);
        }

        [Test]
        public void GetVenues_GetVenuesCount()
        {
            // Arrange

            // Act
            var venues = this.venueService.GetVenues();

            // Assert
            Assert.AreEqual(2, venues.Count());
        }

        [Test]
        public void AddVenue_IsVenueName_GetException()
        {
            // Arrange
            var venueDto = new VenueDto
            {
                Id = 3,
                Description = "№9",
                Name = "first",
                Address = "9",
                Phone = "9",
            };

            // Act - delegate. Assert
            Assert.Throws<TicketManagementException>(
                () => this.venueService.AddVenue(venueDto));
        }

        [Test]
        public void UpdateVenue_VenueIsNull_GetException()
        {
            // Arrange
            var venueDto = new VenueDto
            {
                Id = 3,
                Description = "№9",
                Name = "first",
                Address = "9",
                Phone = "9",
            };

            // Act - delegate. Assert
            Assert.Throws<TicketManagementException>(
                () => this.venueService.UpdateVenue(venueDto));
        }

        [Test]
        public void UpdateVenue_IsVenueName_GetException()
        {
            // Arrange
            var venueDto = new VenueDto
            {
                Id = 2,
                Description = "№9",
                Name = "first",
                Address = "9",
                Phone = "9",
            };

            // Act - delegate. Assert
            Assert.Throws<TicketManagementException>(
                () => this.venueService.UpdateVenue(venueDto));
        }

        [Test]
        public void GetVenue_VenueIsNull_GetException()
        {
            // Arrange
            // Act - delegate. Assert
            Assert.Throws<TicketManagementException>(
                () => this.venueService.GetVenue(3));
        }
    }
}
