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
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class EventServiceTests : Test
    {
        private IEventService eventService;
        private IEventAreaService eventAreaService;
        private IEventSeatService eventSeatService;

        [SetUp]
        public void Init()
        {
            this.eventService = this.Container.Resolve<IEventService>();
            this.eventAreaService = this.Container.Resolve<IEventAreaService>();
            this.eventSeatService = this.Container.Resolve<IEventSeatService>();
        }

        [Test]
        public void AddEvent_AddNewEventNewAreasEventNewSeatsEvent_GetEvents()
        {
            var eventDto = new Event
            {
                Id = 2,
                BeginDate = new DateTime(2025, 10, 20, 8, 30, 59),
                EndDate = new DateTime(2025, 10, 20, 10, 30, 59),
                Description = "2",
                LayoutId = 2,
                Name = "2",
            };

            int id = this.eventService.AddEvent(eventDto);

            var eventDtoTemp = this.eventService.GetEvent(id);

            Assert.AreEqual("2", eventDtoTemp.Description);
            Assert.AreEqual(new DateTime(2025, 10, 20, 8, 30, 59), eventDtoTemp.BeginDate);
            Assert.AreEqual(new DateTime(2025, 10, 20, 10, 30, 59), eventDtoTemp.EndDate);
            Assert.AreEqual(2, eventDtoTemp.LayoutId);
            Assert.AreEqual("2", eventDtoTemp.Name);
            Assert.AreEqual(6, this.eventAreaService.GetEventAreas().Count());
            Assert.AreEqual(30, this.eventSeatService.GetEventSeats().Count());
        }

        [Test]
        public void UpdateEvent_NewEvent_GetEvent()
        {
            var eventDto = new Event
            {
                Id = 2,
                BeginDate = new DateTime(2025, 10, 20, 8, 30, 59),
                EndDate = new DateTime(2025, 10, 20, 10, 30, 59),
                Description = "2",
                LayoutId = 2,
                Name = "2",
            };
            this.eventService.UpdateEvent(eventDto);

            var eventDtoTemp = this.eventService.GetEvent(2);

            Assert.AreEqual(new DateTime(2025, 10, 20, 8, 30, 59), eventDtoTemp.BeginDate);
            Assert.AreEqual(new DateTime(2025, 10, 20, 10, 30, 59), eventDtoTemp.EndDate);
            Assert.AreEqual("2", eventDtoTemp.Description);
            Assert.AreEqual(2, eventDtoTemp.LayoutId);
            Assert.AreEqual("2", eventDtoTemp.Name);
        }

        [Test]
        public void UpdateEvent_NewEventLayout_DeleteEventAreasAndEventSeatOldLayoutAndInsertEventAreaAndEventSeatForNewLayout()
        {
            var eventDto = new Event
            {
                Id = 2,
                BeginDate = new DateTime(2025, 10, 20, 8, 30, 59),
                EndDate = new DateTime(2025, 10, 20, 10, 30, 59),
                Description = "2",
                LayoutId = 3,
                Name = "2",
            };

            var eventSeatExpected = new List<EventSeat>
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
            var eventAreaExpected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordX = 1, CoordY = 1, Description = "First area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordX = 1, CoordY = 1, Description = "Second area of first layout", EventId = 1, Price = 100 },
                new EventArea() { Id = 5, CoordX = 1, CoordY = 1, Description = "First area of third layout", EventId = 2, Price = 0 },
                new EventArea() { Id = 6, CoordX = 2, CoordY = 2, Description = "Second area of third layout", EventId = 2, Price = 0 },
            };

            this.eventService.UpdateEvent(eventDto);

            var eventDtoTemp = this.eventService.GetEvent(2);

            Assert.AreEqual(new DateTime(2025, 10, 20, 8, 30, 59), eventDtoTemp.BeginDate);
            Assert.AreEqual(new DateTime(2025, 10, 20, 10, 30, 59), eventDtoTemp.EndDate);
            Assert.AreEqual("2", eventDtoTemp.Description);
            Assert.AreEqual(3, eventDtoTemp.LayoutId);
            Assert.AreEqual("2", eventDtoTemp.Name);

            eventSeatExpected.Should().BeEquivalentTo(this.eventSeatService.GetEventSeats());
            eventAreaExpected.Should().BeEquivalentTo(this.eventAreaService.GetEventAreas());
        }

        [Test]
        public void DeleteEvent_EventId_GetEventsCount()
        {
            this.eventService.DeleteEvent(2);

            int eventDtoTemp = this.eventService.GetEvents().Count();
            Assert.AreEqual(1, eventDtoTemp);
            Assert.AreEqual(2, this.eventAreaService.GetEventAreas().Count());
            Assert.AreEqual(10, this.eventSeatService.GetEventSeats().Count());
        }

        [Test]
        public void GetEvent_EventId_GetEvent()
        {
            Event eventDtoTemp = this.eventService.GetEvent(1);

            Assert.AreEqual("First", eventDtoTemp.Description);
            Assert.AreEqual("First event", eventDtoTemp.Name);
            Assert.AreEqual(1, eventDtoTemp.LayoutId);
            Assert.AreEqual(new DateTime(2025, 12, 12, 12, 00, 00), eventDtoTemp.BeginDate);
            Assert.AreEqual(new DateTime(2025, 12, 12, 13, 00, 00), eventDtoTemp.EndDate);
        }

        [Test]
        public void GetEvents_GetEventsCount()
        {
            int eventDtoCount = this.eventService.GetEvents().Count();

            Assert.AreEqual(2, eventDtoCount);
        }
    }
}
