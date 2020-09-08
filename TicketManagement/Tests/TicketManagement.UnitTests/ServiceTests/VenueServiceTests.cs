// ****************************************************************************
// <copyright file="VenueServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using Autofac;
using AutoFixture;
using Moq;
using NUnit.Framework;
using TicketManagement.BLL.DTO;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories.Util;
using TicketManagement.UnitTests.ServiceTests.Base;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class VenueServiceTests : TestWithRepositoryBase<Venue>
    {
        private IVenueService venueService;

        public VenueServiceTests()
            : base(InitializeFakeData.InitializeVenueData())
        {
        }

        [SetUp]
        public void Init()
        {
            this.venueService = this.Container.Resolve<IVenueService>();
        }

        [Test]
        public void AddVenue_AddNewVenue_GetCountVenues()
        {
            // Arrange
            int expectedVenuesCount = this.FakeRepositoryData.Count() + 1;
            var venueDto = this.Fixture.Build<VenueDto>().Create();

            // Act
            this.venueService.AddVenue(venueDto);

            // Assert
            Assert.Multiple(() =>
            {
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Create(It.IsAny<Venue>()), Times.Once);
                Assert.AreEqual(expectedVenuesCount, this.FakeRepositoryData.Count());
            });
        }

        [Test]
        public void UpdateVenue_NewVenue_GetVenueProperties()
        {
            // Arrange
            var venueId = 1;
            var venueDto = this.Fixture.Build<VenueDto>().With(e => e.Id, venueId).Create();

            // Act
            this.venueService.UpdateVenue(venueDto);

            // Assert
            Assert.Multiple(() =>
            {
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Update(It.IsAny<Venue>()), Times.Once);
                var venue = this.FakeRepositoryData.First(x => x.Id == venueId);
                Assert.AreEqual(venueDto.Description, venue.Description);
                Assert.AreEqual(venueDto.Address, venue.Address);
                Assert.AreEqual(venueDto.Name, venue.Name);
                Assert.AreEqual(venueDto.Phone, venue.Phone);
            });
        }

        [Test]
        public void DeleteVenue_VenueId_GetVenuesCount()
        {
            // Arrange
            int venueId = 1;
            int expectedCount = this.FakeRepositoryData.Count() - 1;

            // Act
            this.venueService.DeleteVenue(venueId);

            // Assert
            Assert.Multiple(() =>
            {
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
                Assert.AreEqual(expectedCount, this.FakeRepositoryData.Count());
            });
        }

        [Test]
        public void GetVenue_VenueId_GetVenueProperties()
        {
            // Arrange
            var venueId = 1;
            var expectedVenue = this.FakeRepositoryData.First(x => x.Id == venueId);

            // Act
            var venueDto = this.venueService.GetVenue(venueId);

            // Assert
            Assert.Multiple(() =>
            {
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
                Assert.AreEqual(expectedVenue.Description, venueDto.Description);
                Assert.AreEqual(expectedVenue.Address, venueDto.Address);
                Assert.AreEqual(expectedVenue.Name, venueDto.Name);
                Assert.AreEqual(expectedVenue.Phone, venueDto.Phone);
            });
        }

        [Test]
        public void GetVenues_GetVenuesCount()
        {
            // Arrange
            var expectedCount = this.FakeRepositoryData.Count();

            // Act
            var venues = this.venueService.GetVenues();

            // Assert
            Assert.Multiple(() =>
            {
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                Assert.AreEqual(expectedCount, venues.Count());
            });
        }

        [Test]
        public void AddVenue_IsVenueName_GetException()
        {
            // Arrange
            var existingVenueName = this.FakeRepositoryData.First().Name;
            var venueDto = this.Fixture.Build<VenueDto>().With(e => e.Name, existingVenueName).Create();

            // Act - delegate. Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<TicketManagementException>(
                    () => this.venueService.AddVenue(venueDto));
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Create(It.IsAny<Venue>()), Times.Never);
            });
        }

        [Test]
        public void UpdateVenue_VenueIsNull_GetException()
        {
            // Arrange
            var idNotExist = -1;
            var venueDto = this.Fixture.Build<VenueDto>().With(e => e.Id, idNotExist).Create();

            // Act - delegate. Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<TicketManagementException>(
                () => this.venueService.UpdateVenue(venueDto));
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Update(It.IsAny<Venue>()), Times.Once);
            });
        }

        [Test]
        public void UpdateVenue_IsVenueName_GetException()
        {
            // Arrange
            var existingVenueName = this.FakeRepositoryData.First().Name;
            var venueIdForUpdate = this.FakeRepositoryData.Last().Id;
            var venueDto = this.Fixture.Build<VenueDto>().With(e => e.Id, venueIdForUpdate).With(e => e.Name, existingVenueName).Create();

            // Act - delegate. Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<TicketManagementException>(
                () => this.venueService.UpdateVenue(venueDto));
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetAll(), Times.Once);
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Update(It.IsAny<Venue>()), Times.Never);
            });
        }

        [Test]
        public void GetVenue_VenueIsNull_GetException()
        {
            var idNotExist = -1;

            // Arrange
            // Act - delegate. Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<TicketManagementException>(
                    () => this.venueService.GetVenue(idNotExist));
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            });
        }

        [Test]
        public void DeleteVenue_VenueIsNull_GetException()
        {
            var idNotExist = 10;

            // Arrange
            // Act - delegate. Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<TicketManagementException>(
                    () => this.venueService.DeleteVenue(idNotExist));
                this.Mock.Mock<IRepository<Venue>>().Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
            });
        }
    }
}
