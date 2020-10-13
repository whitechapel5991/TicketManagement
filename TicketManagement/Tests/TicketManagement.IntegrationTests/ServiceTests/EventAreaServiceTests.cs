// ****************************************************************************
// <copyright file="EventAreaServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
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
    internal class EventAreaServiceTests : Test
    {
        private IEventAreaService eventAreaService;
        private IRepository<EventArea> eventAreaRepository;
        private IRepository<EventSeat> eventSeatRepository;

        [SetUp]
        public void Init()
        {
            this.eventAreaService = this.Container.Resolve<IEventAreaService>();
            this.eventAreaRepository = this.Container.Resolve<IRepository<EventArea>>();
            this.eventSeatRepository = this.Container.Resolve<IRepository<EventSeat>>();
        }

        [Test]
        public void UpdateEventArea_WhenUpdateEventAreaWithExistingId_ShouldBeUpdateOnlyEventAreaPrice()
        {
            // Arrange
            var expected = new EventArea
            {
                Id = 1,
                Description = "blab-la5",
                CoordinateX = 1000,
                CoordinateY = 1000,
                Price = 1000,
                EventId = 2,
            };

            // Act
            this.eventAreaService.UpdateEventArea(expected);

            // Assert
            var actual = this.eventAreaRepository.GetById(expected.Id);
            expected.Should().BeEquivalentTo(actual, option => option
                .Including(p => p.Price)
                .ExcludingMissingMembers());
            expected.Should().NotBeEquivalentTo(actual, option => option
                .Including(p => p.CoordinateX)
                .Including(p => p.CoordinateY)
                .Including(p => p.Description)
                .Including(p => p.EventId)
                .ExcludingMissingMembers());
        }

        [Test]
        public void GetEventArea_WhenGetEventAreaWithExistingId_ShouldBeReturnEventArea()
        {
            // Arrange
            const int eventAreaId = 1;
            var expected = new EventArea() { Id = eventAreaId, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 1, Price = 100 };

            // Act
            var actual = this.eventAreaService.GetEventArea(eventAreaId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventAreas_WhenGetEventAreas_ShouldBeReturnAllEventAreas()
        {
            // Arrange
            var expected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordinateX = 2, CoordinateY = 2, Description = "First area of second layout", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Second area of second layout", EventId = 2, Price = 200 },
            };

            // Act
            var actual = this.eventAreaService.GetEventAreas();

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }
    }
}
