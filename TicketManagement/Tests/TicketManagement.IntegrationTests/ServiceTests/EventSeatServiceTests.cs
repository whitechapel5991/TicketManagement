// ****************************************************************************
// <copyright file="EventSeatServiceTests.cs" company="EPAM Systems">
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
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class EventSeatServiceTests : Test
    {
        private IEventSeatService eventSeatService;
        private IRepository<EventSeat> eventSeatRepository;

        [SetUp]
        public void Init()
        {
            this.eventSeatService = this.Container.Resolve<IEventSeatService>();
            this.eventSeatRepository = this.Container.Resolve<IRepository<EventSeat>>();
        }

        [Test]
        public void UpdateEventSeat_WhenUpdateEventSeatWithExistingId_ShouldBeUpdateOnlyEventSeatState()
        {
            // Arrange
            var expected = new EventSeat
            {
                Id = 1,
                Number = 2,
                State = EventSeatState.Sold,
                Row = 2,
                EventAreaId = 2,
            };

            // Act
            this.eventSeatService.UpdateEventSeat(expected);

            // Assert
            var actual = this.eventSeatRepository.GetById(expected.Id);
            actual.Should().BeEquivalentTo(expected, option => option
                .Including(p => p.State)
                .ExcludingMissingMembers());
            actual.Should().NotBeEquivalentTo(expected, option => option
                .Including(p => p.Row)
                .Including(p => p.Number)
                .Including(p => p.EventAreaId)
                .ExcludingMissingMembers());
        }

        [Test]
        public void GetEventSeat_WhenGetEventSeatWithExistingEventSeatId_ShouldBeReturnThisEventSeat()
        {
            // Arrange
            const int existingEventSeatId = 1;
            var expected = new EventSeat() { Id = existingEventSeatId, State = EventSeatState.InBasket, EventAreaId = 1, Row = 1, Number = 1 };

            // Act
            var actual = this.eventSeatService.GetEventSeat(existingEventSeatId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventSeats_WhenGetEventSeats_ShouldBeReturnAllEventSeats()
        {
            // Arrange
            var expected = new List<EventSeat>
            {
                new EventSeat() { Id = 1, State = EventSeatState.InBasket, EventAreaId = 1, Row = 1, Number = 1 },
                new EventSeat() { Id = 2, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 2 },
                new EventSeat() { Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3 },
                new EventSeat() { Id = 4, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 4 },
                new EventSeat() { Id = 5, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 5 },
                new EventSeat() { Id = 6, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 1 },
                new EventSeat() { Id = 7, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 2 },
                new EventSeat() { Id = 8, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 3 },
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
    }
}
