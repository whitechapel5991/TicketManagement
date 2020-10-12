// ****************************************************************************
// <copyright file="EventServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class EventServiceTests : Test
    {
        private IEventService eventService;

        private IRepository<Event> eventRepository;
        private IRepository<EventArea> eventAreaRepository;
        private IRepository<EventSeat> eventSeatRepository;

        [SetUp]
        public void Init()
        {
            this.eventService = this.Container.Resolve<IEventService>();
            this.eventRepository = this.Container.Resolve<IRepository<Event>>();
            this.eventAreaRepository = this.Container.Resolve<IRepository<EventArea>>();
            this.eventSeatRepository = this.Container.Resolve<IRepository<EventSeat>>();
        }

        [Test]
        public void AddEvent_WhenAddNewEvent_ShouldBeSaveNewEventAndEventAreasAndEventSeatsInRepository()
        {
            // Arrange
            var newEvent = new Event
            {
                Id = 3,
                BeginDate = new DateTime(2025, 10, 20, 8, 30, 59),
                EndDate = new DateTime(2025, 10, 20, 10, 30, 59),
                Description = "2",
                LayoutId = 2,
                Name = "2",
            };
            var expectedEvents = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = true,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = false,
                },
                newEvent,
            };
            var expectedEventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordinateX = 2, CoordinateY = 2, Description = "First area of second layout", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Second area of second layout", EventId = 2, Price = 200 },
                new EventArea() { Id = 5, CoordinateX = 3, CoordinateY = 3, Description = "First area of second layout", EventId = 3, Price = 0 },
                new EventArea() { Id = 6, CoordinateX = 4, CoordinateY = 4, Description = "Second area of second layout", EventId = 3, Price = 0 },
            };
            var expectedEventSeats = new List<EventSeat>
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
                new EventSeat() { Id = 21, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 1 },
                new EventSeat() { Id = 22, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 2 },
                new EventSeat() { Id = 23, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 3 },
                new EventSeat() { Id = 24, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 4 },
                new EventSeat() { Id = 25, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 5 },
                new EventSeat() { Id = 26, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 1 },
                new EventSeat() { Id = 27, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 2 },
                new EventSeat() { Id = 28, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 3 },
                new EventSeat() { Id = 29, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 4 },
                new EventSeat() { Id = 30, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 5 },
            };

            // Act
            this.eventService.AddEvent(newEvent);

            // Assert
            using (new AssertionScope())
            {
                this.eventRepository.GetAll().Should().BeEquivalentTo(expectedEvents);
                this.eventAreaRepository.GetAll().Should().BeEquivalentTo(expectedEventAreas);
                this.eventSeatRepository.GetAll().Should().BeEquivalentTo(expectedEventSeats);
            }
        }

        [Test]
        public void UpdateEvent_WhenUpdateEventWithExistingId_ShouldBeUpdateAllFieldsAndEventAreasAndEventSeatsInTheRepositoryDeleteEventAreasAndEventSeatOldLayoutAndInsertEventAreaAndEventSeatForNewLayout()
        {
            // Arrange
            var eventDto = new Event
            {
                Id = 2,
                BeginDate = new DateTime(2025, 10, 20, 8, 30, 59),
                EndDate = new DateTime(2025, 10, 20, 10, 30, 59),
                Description = "2",
                LayoutId = 1,
                Name = "2",
            };
            var expectedEvents = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = true,
                },
                eventDto,
            };
            var expectedEventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 5, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 2, Price = 0 },
                new EventArea() { Id = 6, CoordinateX = 2, CoordinateY = 2, Description = "Second area of first layout", EventId = 2, Price = 0 },
            };
            var expectedEventSeats = new List<EventSeat>
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
                new EventSeat() { Id = 21, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 1 },
                new EventSeat() { Id = 22, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 2 },
                new EventSeat() { Id = 23, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 3 },
                new EventSeat() { Id = 24, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 4 },
                new EventSeat() { Id = 25, State = EventSeatState.Free, EventAreaId = 5, Row = 1, Number = 5 },
                new EventSeat() { Id = 26, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 1 },
                new EventSeat() { Id = 27, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 2 },
                new EventSeat() { Id = 28, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 3 },
                new EventSeat() { Id = 29, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 4 },
                new EventSeat() { Id = 30, State = EventSeatState.Free, EventAreaId = 6, Row = 1, Number = 5 },
            };

            // Act
            this.eventService.UpdateEvent(eventDto);

            // Assert
            using (new AssertionScope())
            {
                this.eventRepository.GetAll().Should().BeEquivalentTo(expectedEvents);
                this.eventAreaRepository.GetAll().Should().BeEquivalentTo(expectedEventAreas);
                this.eventSeatRepository.GetAll().Should().BeEquivalentTo(expectedEventSeats);
            }
        }

        [Test]
        public void DeleteEvent_WhenDeleteEventWithExistingEventId_ShouldBeDeleteEventAndEventAreasAndEventSeatsFromTheRepository()
        {
            // Arrange
            const int eventId = 2;
            var expectedEvents = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = true,
                },
            };
            var expectedEventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area of first layout", EventId = 1, Price = 100 },
            };
            var expectedEventSeats = new List<EventSeat>
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
            };

            // Act
            this.eventService.DeleteEvent(eventId);

            // Assert
            using (new AssertionScope())
            {
                this.eventRepository.GetAll().Should().BeEquivalentTo(expectedEvents);
                this.eventAreaRepository.GetAll().Should().BeEquivalentTo(expectedEventAreas);
                this.eventSeatRepository.GetAll().Should().BeEquivalentTo(expectedEventSeats);
            }
        }

        [Test]
        public void GetEvent_WhenGetEventWithExistingEventId_ShouldBeReturnThisEvent()
        {
            // Arrange
            const int eventId = 2;
            var expected = new Event()
            {
                Id = 2,
                BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "Second",
                LayoutId = 2,
                Name = "Second event",
                Published = false,
            };

            // Act
            var actual = this.eventService.GetEvent(eventId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEvents_WhenGetEvents_ShouldBeReturnAllEvents()
        {
            // Arrange
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = true,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = false,
                },
            };

            // Act
            var actual = this.eventService.GetEvents();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetPublishEvents_WhenGetPublishEvents_ShouldBeReturnAllPublishEvents()
        {
            // Arrange
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = true,
                },
            };

            // Act
            var actual = this.eventService.GetPublishEvents();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAvailableSeatCount_WhenGetAvailableSeatCountWithExistingEventId_ShouldBeReturnAvailableSeatCount()
        {
            // Arrange
            const int existingEventId = 1;
            const int expectedCount = 9;

            // Act
            var actualCount = this.eventService.GetAvailableSeatCount(existingEventId);

            // Assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetEventBySeatId_WhenGetEventBySeatIdWithExistingEventSeatId_ShouldBeReturnThisEvent()
        {
            // Arrange
            const int existingEventSeatId = 1;
            var expectedDto = new Event()
            {
                Id = 1,
                BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
                Published = true,
            };

            // Act
            var actualDto = this.eventService.GetEventByEventSeatId(existingEventSeatId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
