// ****************************************************************************
// <copyright file="EventAreaServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.BllServicesTests
{
    [TestFixture]
    internal class EventAreaServiceTests
    {
        private IEventAreaService eventAreaService;

        private IRepository<EventArea> eventAreaRepository;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var eventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordinateX = 2, CoordinateY = 2, Description = "First area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Second area event", EventId = 2, Price = 200 },
            };
            var fakeEventAreaRepository = new RepositoryFake<EventArea>(eventAreas);

            var eventSeats = new List<EventSeat>
            {
                new EventSeat() { Id = 1, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1 },
                new EventSeat() { Id = 2, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 2 },
                new EventSeat() { Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3 },
                new EventSeat() { Id = 4, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 4 },
                new EventSeat() { Id = 5, State = EventSeatState.Sold, EventAreaId = 1, Row = 1, Number = 5 },
                new EventSeat() { Id = 6, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 1 },
                new EventSeat() { Id = 7, State = EventSeatState.Sold, EventAreaId = 2, Row = 1, Number = 2 },
                new EventSeat() { Id = 8, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 3 },
                new EventSeat() { Id = 9, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 4 },
                new EventSeat() { Id = 10, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 5 },
                new EventSeat() { Id = 11, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 1 },
                new EventSeat() { Id = 12, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 2 },
                new EventSeat() { Id = 13, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 3 },
                new EventSeat() { Id = 14, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 4 },
                new EventSeat() { Id = 15, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 5 },
                new EventSeat() { Id = 16, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 1 },
                new EventSeat() { Id = 17, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 2 },
                new EventSeat() { Id = 18, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 3 },
                new EventSeat() { Id = 19, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 4 },
                new EventSeat() { Id = 20, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 5 },
            };
            var fakeEventSeatRepository = new RepositoryFake<EventSeat>(eventSeats);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeEventAreaRepository)
                    .As<IRepository<EventArea>>();
                builder.RegisterInstance(fakeEventSeatRepository)
                    .As<IRepository<EventSeat>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void UpdateEventArea_WhenUpdateEventAreaWithExistingId_ShouldBeUpdateOnlyEventAreaPrice()
        {
            // Arrange
            this.eventAreaRepository = this.Mock.Create<IRepository<EventArea>>();
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            const int existingEventAreaId = 1;
            var expectedDto = this.Fixture.Build<EventArea>()
                .With(e => e.Id, existingEventAreaId)
                .Create();

            // Act
            this.eventAreaService.UpdateEventArea(expectedDto);

            // Assert
            var actualEventArea = this.eventAreaRepository.GetById(existingEventAreaId);
            using (new AssertionScope())
            {
                actualEventArea.Should().BeEquivalentTo(expectedDto, option => option
                    .Including(p => p.Price)
                    .ExcludingMissingMembers());
                actualEventArea.Should().NotBeEquivalentTo(expectedDto, option => option
                    .Including(p => p.CoordinateX)
                    .Including(p => p.CoordinateY)
                    .Including(p => p.Description)
                    .Including(p => p.EventId)
                    .ExcludingMissingMembers());
            }
        }

        [Test]
        public void GetEventArea_WhenGetEventAreaWithExistingEventAreaId_ShouldBeReturnThisEventArea()
        {
            // Arrange
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            const int existingEventAreaId = 1;
            var expectedDto = new EventArea() { Id = existingEventAreaId, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 };

            // Act
            var actualDto = this.eventAreaService.GetEventArea(existingEventAreaId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEventAreas_WhenGetEventAreas_ShouldBeReturnAllEventAreas()
        {
            // Arrange
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            var expected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordinateX = 2, CoordinateY = 2, Description = "First area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Second area event", EventId = 2, Price = 200 },
            };

            // Act
            var actual = this.eventAreaService.GetEventAreas();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAreaCost_WhenGetAreaCostWithExistingEventSeatId_ShouldBeReturnEventAreaCost()
        {
            // Arrange
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            const int existingEventSeatId = 1;
            const decimal expectedEventAreaCost = 100;

            // Act
            var actualEventAreaCost = this.eventAreaService.GetEventAreaCost(existingEventSeatId);

            // Assert
            actualEventAreaCost.Should().Be(expectedEventAreaCost);
        }

        [Test]
        public void GetEventAreasByEventSeatIds_WhenGetEventAreasByEventSeatIds_ShouldBeReturnAllEventAreaContainsTheseIds()
        {
            // Arrange
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            int[] eventSeatIdArray = { 1, 2, 3, 8 };
            var expected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 1, Price = 100 },
            };

            // Act
            var actual = this.eventAreaService.GetEventAreasByEventSeatIds(eventSeatIdArray);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventAreasByEventId_WhenGetEventAreasByEventId_ShouldBeReturnAllEventAreaWithEventId()
        {
            // Arrange
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            const int eventId = 1;
            var expected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 1, Price = 100 },
            };

            // Act
            var actual = this.eventAreaService.GetEventAreasByEventId(eventId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
