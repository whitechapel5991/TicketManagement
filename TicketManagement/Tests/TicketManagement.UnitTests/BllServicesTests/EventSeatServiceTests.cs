// ****************************************************************************
// <copyright file="EventSeatServiceTests.cs" company="EPAM Systems">
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
    internal class EventSeatServiceTests
    {
        private IEventSeatService eventSeatService;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

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
        public void UpdateEventSeat_WhenUpdateEventSeatWithExistingId_ShouldBeUpdateOnlyEventSeatState()
        {
            // Arrange
            var eventSeatRepository = this.Mock.Create<IRepository<EventSeat>>();
            this.eventSeatService = this.Mock.Create<EventSeatService>();
            const int existingEventSeatId = 1;
            var expectedDto = this.Fixture.Build<EventSeat>()
                .With(e => e.Id, existingEventSeatId)
                .Create();

            // Act
            this.eventSeatService.UpdateEventSeat(expectedDto);

            // Assert
            using (new AssertionScope())
            {
                var actualEventSeat = eventSeatRepository.GetById(existingEventSeatId);
                actualEventSeat.Should().BeEquivalentTo(expectedDto, option => option
                    .Including(p => p.State)
                    .ExcludingMissingMembers());
                actualEventSeat.Should().NotBeEquivalentTo(expectedDto, option => option
                    .Including(p => p.Row)
                    .Including(p => p.Number)
                    .Including(p => p.EventAreaId)
                    .ExcludingMissingMembers());
            }
        }

        [Test]
        public void GetEventSeat_WhenGetEventSeatWithExistingEventSeatId_ShouldBeReturnThisEventSeat()
        {
            // Arrange
            this.eventSeatService = this.Mock.Create<EventSeatService>();
            const int existingEventSeatId = 1;
            var expectedDto = new EventSeat() { Id = existingEventSeatId, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1 };

            // Act
            var actualDto = this.eventSeatService.GetEventSeat(existingEventSeatId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEventSeats_WhenGetEventSeats_ShouldBeReturnAllEventSeats()
        {
            // Arrange
            this.eventSeatService = this.Mock.Create<EventSeatService>();
            var expected = new List<EventSeat>
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

            // Act
            var actual = this.eventSeatService.GetEventSeats();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventSeatsByEventSeatIds_WhenGetEventSeatsByEventSeatIds_ShouldBeReturnAllEventSeatsContainsTheseIds()
        {
            // Arrange
            this.eventSeatService = this.Mock.Create<EventSeatService>();
            int[] eventSeatId = { 1, 3, 5, 10, 19 };
            var expected = new List<EventSeat>
            {
                new EventSeat() { Id = 1, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1 },
                new EventSeat() { Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3 },
                new EventSeat() { Id = 5, State = EventSeatState.Sold, EventAreaId = 1, Row = 1, Number = 5 },
                new EventSeat() { Id = 10, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 5 },
                new EventSeat() { Id = 19, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 4 },
            };

            // Act
            var actual = this.eventSeatService.GetEventSeatsByEventSeatIds(eventSeatId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventSeatsByEventAreaId_WhenGetEventSeatsByEventAreaId_ShouldBeReturnAllEventSeatsInThisEventArea()
        {
            // Arrange
            this.eventSeatService = this.Mock.Create<EventSeatService>();
            const int eventAreaId = 1;
            var expected = new List<EventSeat>
            {
                new EventSeat() { Id = 1, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1 },
                new EventSeat() { Id = 2, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 2 },
                new EventSeat() { Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3 },
                new EventSeat() { Id = 4, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 4 },
                new EventSeat() { Id = 5, State = EventSeatState.Sold, EventAreaId = 1, Row = 1, Number = 5 },
            };

            // Act
            var actual = this.eventSeatService.GetEventSeatsByEventAreaId(eventAreaId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
