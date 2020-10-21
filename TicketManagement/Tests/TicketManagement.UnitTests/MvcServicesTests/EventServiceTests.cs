using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Models.Event;
using TicketManagement.Web.Services;
using IEventServiceWeb = TicketManagement.Web.Interfaces.IEventService;

namespace TicketManagement.UnitTests.MvcServicesTests
{
    [TestFixture]
    internal class EventServiceTests
    {
        private IEventServiceWeb eventServiceWeb;

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
        public void GetPublishEvents_WhenGetPublishEvents_ShouldBeReturnEventViewModelArray()
        {
            // Arrange
            int[] layoutIdArray = { 1, 2, 3 };
            var eventServiceMock = this.Mock.Mock<IEventService>();
            eventServiceMock.Setup(x => x.GetPublishEvents()).Returns(new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = layoutIdArray[0], Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = layoutIdArray[1], Name = "Second event", Published = true,
                },
                new Event()
                {
                    Id = 3, BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Third",
                    LayoutId = layoutIdArray[2], Name = "Third event", Published = false,
                },
            });

            eventServiceMock.Setup(x => x.GetAvailableSeatCount(1)).Returns(3);
            eventServiceMock.Setup(x => x.GetAvailableSeatCount(2)).Returns(4);
            eventServiceMock.Setup(x => x.GetAvailableSeatCount(3)).Returns(10);

            this.Mock.Mock<ILayoutService>().Setup(x => x.GetLayoutsByLayoutIds(layoutIdArray)).Returns(new List<Layout>
            {
                new Layout() {Id = 1, Name = "first", Description = "First layout", VenueId = 1},
                new Layout() {Id = 2, Name = "second", Description = "Second layout", VenueId = 1},
                new Layout() {Id = 3, Name = "third", Description = "First layout", VenueId = 2},
            });

            this.eventServiceWeb = this.Mock.Create<EventService>();

            var expected = new List<EventViewModel>
            {
                new EventViewModel
                {
                    Id = 1,
                    Name = "First event",
                    Description = "First",
                    BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    BeginTime = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndTime = new DateTime(2025, 12, 12, 13, 00, 00),
                    CountFreeSeats = 3,
                    LayoutName = "first",
                },
                new EventViewModel
                {
                    Id = 2,
                    Name = "Second event",
                    Description = "Second",
                    BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00),
                    BeginTime = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndTime = new DateTime(2025, 12, 12, 14, 00, 00),
                    CountFreeSeats = 4,
                    LayoutName = "second",
                },
                new EventViewModel
                {
                    Id = 3,
                    Name = "Third event",
                    Description = "Third",
                    BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00),
                    BeginTime = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndTime = new DateTime(2025, 12, 12, 14, 00, 00),
                    CountFreeSeats = 10,
                    LayoutName = "third",
                },
            };

            // Act
            var actual = this.eventServiceWeb.GetPublishEvents();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventDetailViewModel_WhenGetEventDetailViewModelByEventId_ShouldBeReturnEventDetailViewModel()
        {
            // Arrange
            const int eventId = 1;
            const int layoutId = 1;
            var eventServiceMock = this.Mock.Mock<IEventService>();
            eventServiceMock.Setup(x => x.GetEvent(eventId)).Returns(  new Event()
            {
                Id = eventId, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                LayoutId = layoutId, Name = "First event", Published = false,
            });

            this.Mock.Mock<ILayoutService>().Setup(x => x.GetLayout(layoutId)).Returns(new Layout()
                {Id = layoutId, Name = "first", Description = "First layout", VenueId = 1});

            this.Mock.Mock<IEventAreaService>().Setup(x => x.GetEventAreasByEventId(eventId)).Returns(new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = eventId, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = eventId, Price = 150 },
            });

            this.eventServiceWeb = this.Mock.Create<EventService>();

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
            var actual = this.eventServiceWeb.GetEventDetailViewModel(eventId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventAreaDetailViewModel_WhenGetEventAreaDetailViewModelByEventAreaId_ShouldBeReturnEventAreaDetailViewModel()
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

            this.eventServiceWeb = this.Mock.Create<EventService>();

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
            var actual = this.eventServiceWeb.GetEventAreaDetailViewModel(eventAreaId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
