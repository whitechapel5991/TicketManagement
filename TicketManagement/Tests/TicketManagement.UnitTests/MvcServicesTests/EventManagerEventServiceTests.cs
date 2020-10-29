using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Event;
using TicketManagement.Web.Services;
using EventViewModel = TicketManagement.Web.Areas.EventManager.Data.EventViewModel;
using IEventService = TicketManagement.BLL.Interfaces.IEventService;

namespace TicketManagement.UnitTests.MvcServicesTests
{
    [TestFixture]
    internal class EventManagerEventServiceTests
    {
        private IEventManagerEventService eventManagerEventService;

        private AutoMock Mock { get; set; }

        [SetUp]
        public void Init()
        {
            this.Mock = AutoMock.GetLoose();
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void GetEventViewModel_WhenGetEventViewModelByEventId_ShouldBeReturnEventViewModel()
        {
            // Arrange
            const int eventId = 1;
            this.Mock.Mock<IEventService>().Setup(x => x.GetEvent(eventId)).Returns(  new Event()
            {
                Id = eventId, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                LayoutId = 1, Name = "First event", Published = false,
            });
            var layoutList = new List<Layout>
            {
                new Layout() {Id = 1, Name = "first", Description = "First layout", VenueId = 1},
                new Layout() {Id = 2, Name = "second", Description = "Second layout", VenueId = 1},
                new Layout() {Id = 3, Name = "third", Description = "First layout", VenueId = 2},
                new Layout() {Id = 4, Name = "forth", Description = "Second layout", VenueId = 2},
                new Layout() {Id = 100, Name = "forth2", Description = "Second layout", VenueId = 2},
                new Layout() {Id = 101, Name = "forth1", Description = "Second layout", VenueId = 2},
            };
            this.Mock.Mock<ILayoutService>().Setup(x => x.GetLayouts()).Returns(layoutList);

            this.eventManagerEventService = this.Mock.Create<EventManagerEventService>();

            var expected = new EventViewModel
            {
                IndexEventViewModel = new IndexEventViewModel
                {
                    Id = eventId,
                    BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    BeginTime = new DateTime(2025, 12, 12, 12, 00, 00),
                    Description = "First",
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndTime = new DateTime(2025, 12, 12, 13, 00, 00),
                    LayoutId = 1,
                    Name = "First event",
                    Published = false,
                    LayoutName = "first",
                },
                LayoutId = 1,
            };

            // Act
            var actual = this.eventManagerEventService.GetEventViewModel(eventId);

            // Assert
            actual.Should().BeEquivalentTo(expected, option => option.Excluding(prop => prop.LayoutList));
        }

        [Test]
        public void GetEventDetailViewModel_WhenGetEventDetailViewModelByEventId_ShouldBeReturnEventDetailViewModel()
        {
            // Arrange
            const int eventId = 1;
            const int layoutId = 1;

            this.Mock.Mock<IEventService>().Setup(x => x.GetEvent(eventId)).Returns(  new Event()
            {
                Id = eventId, BeginDateUtc = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDateUtc = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                LayoutId = layoutId, Name = "First event", Published = false,
            });

            this.Mock.Mock<ILayoutService>().Setup(x => x.GetLayout(layoutId))
                .Returns(new Layout()
            {
                Id = layoutId, Name = "first", Description = "First layout", VenueId = 1,
            });

            this.Mock.Mock<IEventAreaService>().Setup(x => x.GetEventAreasByEventId(eventId)).Returns(new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = eventId, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = eventId, Price = 150 },
            });

            this.eventManagerEventService = this.Mock.Create<EventManagerEventService>();

            var expected = new EventDetailViewModel
            {
                Name = "First event",
                Description = "First",
                BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                LayoutName = "first",
                EventAreas = new List<EventAreaViewModel>
                {
                    new EventAreaViewModel
                    {
                        Id = 1,
                        Description = "First area event",
                        CoordinateX = 1,
                        CoordinateY = 1,
                        Price = 100,
                    },
                    new EventAreaViewModel
                    {
                        Id = 2,
                        Description = "Second area event",
                        CoordinateX = 1,
                        CoordinateY = 1,
                        Price = 150,
                    },
                },
            };

            // Act
            var actual = this.eventManagerEventService.GetEventDetailViewModel(eventId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventAreaDetailViewModel_WhenGetEventAreaDetailViewModelByEventAreaId_ShouldBeReturnGetEventAreaDetailViewModel()
        {
            // Arrange
            const int eventAreaId = 1;

            this.Mock.Mock<IEventAreaService>().Setup(x => x.GetEventArea(eventAreaId)).Returns(new EventArea()
                { Id = eventAreaId, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 });

            this.Mock.Mock<IEventSeatService>().Setup(x => x.GetEventSeatsByEventAreaId(eventAreaId)).Returns(new List<EventSeat>
            {
                new EventSeat() { Id = 1, State = EventSeatState.Free, EventAreaId = eventAreaId, Row = 1, Number = 5 },
                new EventSeat() { Id = 2, State = EventSeatState.InBasket, EventAreaId = eventAreaId, Row = 1, Number = 1 },
                new EventSeat() { Id = 3, State = EventSeatState.Sold, EventAreaId = eventAreaId, Row = 1, Number = 2 },
                new EventSeat() { Id = 4, State = EventSeatState.InBasket, EventAreaId = eventAreaId, Row = 1, Number = 3 },
            });

            this.eventManagerEventService = this.Mock.Create<EventManagerEventService>();

            var expected = new EventAreaDetailViewModel
            {
                EventArea = new EventAreaViewModel
                {
                    Id = eventAreaId,
                    Description = "First area event",
                    CoordinateX = 1,
                    CoordinateY = 1,
                    Price = 100,
                },
                EventId = 1,
                EventSeats = new List<EventSeatViewModel>
                {
                    new EventSeatViewModel
                    {
                        Id = 1,
                        Row = 1,
                        Number = 5,
                        State = EventSeatState.Free,
                    },
                    new EventSeatViewModel
                    {
                        Id = 2,
                        Row = 1,
                        Number = 1,
                        State = EventSeatState.InBasket,
                    },
                    new EventSeatViewModel
                    {
                        Id = 3,
                        Row = 1,
                        Number = 2,
                        State = EventSeatState.Sold,
                    },
                    new EventSeatViewModel
                    {
                        Id = 4,
                        Row = 1,
                        Number = 3,
                        State = EventSeatState.InBasket,
                    },
                },
            };

            // Act
            var actual = this.eventManagerEventService.GetEventAreaDetailViewModel(eventAreaId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
