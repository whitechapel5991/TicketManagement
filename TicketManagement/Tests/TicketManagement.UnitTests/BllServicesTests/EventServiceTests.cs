// ****************************************************************************
// <copyright file="EventServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
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
    internal class EventServiceTests
    {
        private IEventService eventService;

        private IRepository<Event> eventRepository;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var events = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
                new Event()
                {
                    Id = 3, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Third",
                    LayoutId = 3, Name = "Third event", Published = false,
                },
            };
            var fakeVenueRepository = new RepositoryFake<Event>(events);

            var eventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordinateX = 2, CoordinateY = 2, Description = "First area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Second area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordinateX = 2, CoordinateY = 2, Description = "Area event without price", EventId = 2, Price = 0 },
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
                builder.RegisterInstance(fakeVenueRepository)
                    .As<IRepository<Event>>();
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
        public void AddEvent_WhenAddNewEvent_ShouldBeSaveNewEventInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            this.eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var dto = this.Fixture.Build<Event>().Create();
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
                new Event()
                {
                    Id = 3, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Third",
                    LayoutId = 3, Name = "Third event", Published = false,
                },
                dto,
            };

            // Act
            var expectedEntityId = this.eventService.AddEvent(dto);

            // Assert
            using (new AssertionScope())
            {
                this.eventRepository.GetAll().Should().BeEquivalentTo(expected);
                dto.Id.Should().Be(expectedEntityId);
            }
        }

        [Test]
        public void UpdateEvent_WhenUpdateEventWithExistingId_ShouldBeUpdateAllFieldsInTheRepository()
        {
            // Arrange
            this.eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventId = 2;
            var expectedDto = this.Fixture.Build<Event>().With(e => e.Id, existingEventId).Create();

            // Act
            this.eventService.UpdateEvent(expectedDto);

            // Assert
            this.eventRepository.GetById(existingEventId).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void PublishEvent_WhenPublishExistingEvent_ShouldBeUpdatePublishFlagOnTrue()
        {
            // Arrange
            this.eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventId = 1;
            var expectedDto = new Event()
            {
                Id = existingEventId,
                BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
                Published = true,
            };

            // Act
            this.eventService.PublishEvent(existingEventId);

            // Assert
            this.eventRepository.GetById(existingEventId).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteEvent_WhenDeleteEventWithExistingEventId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            this.eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventId = 2;
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event",
                },
                new Event()
                {
                    Id = 3, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Third",
                    LayoutId = 3, Name = "Third event", Published = false,
                },
            };

            // Act
            this.eventService.DeleteEvent(existingEventId);

            // Assert
            this.eventRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEvent_WhenGetEventWithExistingEventId_ShouldBeReturnThisEvent()
        {
            // Arrange
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventId = 1;
            var expectedDto = new Event()
            {
                Id = 1,
                BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
            };

            // Act
            var actualDto = this.eventService.GetEvent(existingEventId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEvents_WhenGetEvents_ShouldBeReturnAllEvents()
        {
            // Arrange
            this.eventService = this.Mock.Create<EventService>();
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
                new Event()
                {
                    Id = 3, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Third",
                    LayoutId = 3, Name = "Third event", Published = false,
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
            this.eventService = this.Mock.Create<EventService>();
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 2, BeginDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
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
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventId = 1;
            const int expectedCount = 6;

            // Act
            var actualCount = this.eventService.GetAvailableSeatCount(existingEventId);

            // Assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetEventBySeatId_WhenGetEventBySeatIdWithExistingEventSeatId_ShouldBeReturnThisEvent()
        {
            // Arrange
            this.eventService = this.Mock.Create<EventService>();
            const int existingEventSeatId = 1;
            var expectedDto = new Event()
            {
                Id = 1,
                BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
            };

            // Act
            var actualDto = this.eventService.GetEventByEventSeatId(existingEventSeatId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEventsByEventSeatIds_WhenGetEventsByEventSeatIds_ShouldBeReturnEventsTheseEventSeats()
        {
            // Arrange
            this.eventService = this.Mock.Create<EventService>();
            int[] existingEventSeatId = { 1, 4, 7, 9 };
            var expectedDto = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
            };

            // Act
            var actualDto = this.eventService.GetEventsByEventSeatIds(existingEventSeatId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
