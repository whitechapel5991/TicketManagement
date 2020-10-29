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
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;
using TicketManagement.Web.Services;

namespace TicketManagement.UnitTests.MvcServicesTests
{
    [TestFixture]
    internal class UserProfileServiceTests
    {
        private IUserProfileService userProfileService;

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
        public void GetPurchaseHistoryViewModel_WhenGetPurchaseHistoryViewModelByUserName_ShouldBeReturnPurchaseHistoryViewModel()
        {
            // Arrange
            int[] eventSeatIdArray = { 5, 6, 9, 7 };

            this.Mock.Mock<IOrderService>().Setup(x => x.GetHistoryOrdersByName("admin")).Returns(new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = eventSeatIdArray[0], DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = eventSeatIdArray[1], DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 3, UserId = 1, EventSeatId = eventSeatIdArray[2], DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 4, UserId = 1, EventSeatId = eventSeatIdArray[3], DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
            });

            this.Mock.Mock<IEventSeatService>().Setup(x => x.GetEventSeatsByEventSeatIds(eventSeatIdArray)).Returns(new List<EventSeat>
            {
                new EventSeat() { Id = eventSeatIdArray[0], State = EventSeatState.InBasket, EventAreaId = 1, Row = 1, Number = 5 },
                new EventSeat() { Id = eventSeatIdArray[1], State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 1 },
                new EventSeat() { Id = eventSeatIdArray[2], State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 2 },
                new EventSeat() { Id = eventSeatIdArray[3], State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 3 },
            });

            this.Mock.Mock<IEventAreaService>().Setup(x => x.GetEventAreasByEventSeatIds(eventSeatIdArray)).Returns(new List<EventArea>
            {
                new EventArea() { Id = 1, CoordinateX = 1, CoordinateY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordinateX = 1, CoordinateY = 1, Description = "Second area event", EventId = 2, Price = 150 },
            });

            this.Mock.Mock<BLL.Interfaces.IEventService>().Setup(x => x.GetEventsByEventSeatIds(eventSeatIdArray)).Returns(new List<Event>
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
            });

            this.userProfileService = this.Mock.Create<UserProfileService>();

            var expected = new PurchaseHistoryViewModel
            {
                Orders = new List<OrderViewModel>()
                {
                    new OrderViewModel
                    {
                        TicketCost = 100,
                        EventName = "First event",
                        EventDescription = "First",
                        DatePurchase = new DateTime(2017, 12, 12, 12, 00, 00),
                    },
                    new OrderViewModel
                    {
                        TicketCost = 150,
                        EventName = "Second event",
                        EventDescription = "Second",
                        DatePurchase = new DateTime(2017, 12, 12, 12, 00, 00),
                    },
                    new OrderViewModel
                    {
                        TicketCost = 150,
                        EventName = "Second event",
                        EventDescription = "Second",
                        DatePurchase = new DateTime(2017, 12, 12, 12, 00, 00),
                    },
                    new OrderViewModel
                    {
                        TicketCost = 150,
                        EventName = "Second event",
                        EventDescription = "Second",
                        DatePurchase = new DateTime(2017, 12, 12, 12, 00, 00),
                    },
                },
            };

            // Act
            var actual = this.userProfileService.GetPurchaseHistoryViewModel("admin");

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
