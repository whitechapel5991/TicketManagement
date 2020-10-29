using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using NUnit.Framework;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Repositories.Identity;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class OrderServiceTests
    {
        private readonly DateTime dateTimeNowMinus20Minutes = DateTime.Now.AddMinutes(-20);

        private IOrderService orderService;

        private IRepository<EventSeat> eventSeatRepository;

        private IRepository<Order> orderRepository;

        private IUserRepository userRepository;

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
                new EventSeat() { Id = 9, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 4 },
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

            var orders = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = 6, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 3, UserId = 2, EventSeatId = 7, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 4, UserId = 2, EventSeatId = 8, DateUtc = this.dateTimeNowMinus20Minutes },
            };
            var fakeOrderRepository = new RepositoryFake<Order>(orders);

            var users = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 1, UserName = "admin", Email = "admin@admin.com", PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Admin", Surname = "Admin", Balance = 100000, SecurityStamp = "SomeSecureStamp" },
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 300 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
            };
            var fakeUserRepository = new UserRepositoryFake(users);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeVenueRepository)
                    .As<IRepository<Event>>();
                builder.RegisterInstance(fakeEventAreaRepository)
                    .As<IRepository<EventArea>>();
                builder.RegisterInstance(fakeEventSeatRepository)
                    .As<IRepository<EventSeat>>();
                builder.RegisterInstance(fakeOrderRepository)
                    .As<IRepository<Order>>();
                builder.RegisterInstance(fakeUserRepository)
                    .As<IUserRepository>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddToCart_WhenAddToCartExistingEventSeat_ShouldBeSaveOrderInRepositoryAndChangeEventSeatStateToInBasketAndScheduleUnlockSeatJob()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;
            this.Mock.Mock<IDataTimeHelper>().Setup(x => x.GetDateTimeUtcNow()).Returns(dateTimeNow);
            var backGroundJob = this.Mock.Mock<IBackgroundJobClient>();
            this.orderRepository = this.Mock.Create<IRepository<Order>>();
            this.eventSeatRepository = this.Mock.Create<IRepository<EventSeat>>();
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const int existingEventSeatId = 1;
            const int userId = 1;

            var expected = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = 6, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 3, UserId = 2, EventSeatId = 7, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 4, UserId = 2, EventSeatId = 8, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { EventSeatId = existingEventSeatId, UserId = userId, DateUtc = dateTimeNow },
            };

            // Act
            this.orderService.AddToCart(existingEventSeatId, userId);

            // Assert
            using (new AssertionScope())
            {
                this.orderRepository.GetAll().Should().BeEquivalentTo(expected);
                this.eventSeatRepository.GetById(existingEventSeatId).State.Should()
                    .BeEquivalentTo(EventSeatState.InBasket);
                backGroundJob.Verify(x => x.Create(
                    It.Is<Job>(job => job.Method.Name == "UnlockSeat" && (int)job.Args[0] == 0),
                    It.IsAny<ScheduledState>()));
            }
        }

        [Test]
        public void Buy_WhenBuySeatWithExistingOrderId_ShouldBeUpdateOrderInRepositoryAndChangeEventSeatStateToSoldAndOrderDateToNowAndInvokeSendEmailAndUpdateUserBalance()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;
            this.Mock.Mock<IDataTimeHelper>().Setup(x => x.GetDateTimeUtcNow()).Returns(dateTimeNow);
            this.Mock.Mock<IRepository<Layout>>().Setup(x => x.GetById(It.IsAny<int>())).Returns(this.Fixture.Build<Layout>().Create());
            var emailHelper = this.Mock.Mock<IEmailHelper>();
            this.orderRepository = this.Mock.Create<IRepository<Order>>();
            this.eventSeatRepository = this.Mock.Create<IRepository<EventSeat>>();
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const int existingOrderId = 2;
            const int eventSeatId = 6;
            const int userId = 1;
            const string userEmail = "admin@admin.com";
            const decimal expectedBalance = 100000 - 100;

            var expected = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = userId, EventSeatId = eventSeatId, DateUtc = dateTimeNow },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 3, UserId = 2, EventSeatId = 7, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 4, UserId = 2, EventSeatId = 8, DateUtc = this.dateTimeNowMinus20Minutes },
            };

            // Act
            this.orderService.Buy(existingOrderId);

            // Assert
            using (new AssertionScope())
            {
                this.orderRepository.GetAll().Should().BeEquivalentTo(expected);
                this.eventSeatRepository.GetById(eventSeatId).State.Should()
                    .BeEquivalentTo(EventSeatState.Sold);
                emailHelper.Verify(x => x.SendEmail(userEmail, It.IsAny<string>(), It.IsAny<string>()));
                this.userRepository.GetById(userId).Balance.Should().Be(expectedBalance);
            }
        }

        [Test]
        public void DeleteFromCart_WhenDeleteSeatFromCartWithExistingOrderId_ShouldBeDeleteOrderFromRepositoryAndChangeEventSeatStateToFree()
        {
            // Arrange
            this.orderRepository = this.Mock.Create<IRepository<Order>>();
            this.eventSeatRepository = this.Mock.Create<IRepository<EventSeat>>();
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const int existingOrderId = 2;
            const int eventSeatId = 6;

            var expected = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 3, UserId = 2, EventSeatId = 7, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 4, UserId = 2, EventSeatId = 8, DateUtc = this.dateTimeNowMinus20Minutes },
            };

            // Act
            this.orderService.DeleteFromCart(existingOrderId);

            // Assert
            using (new AssertionScope())
            {
                this.orderRepository.GetAll().Should().BeEquivalentTo(expected);
                this.eventSeatRepository.GetById(eventSeatId).State.Should()
                    .BeEquivalentTo(EventSeatState.Free);
            }
        }

        [Test]
        public void GetCartOrdersById_WhenGetCartOrderWithExistingUserId_ShouldBeReturnAllUserOrdersInBasket()
        {
            // Arrange
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const int userId = 1;

            var expected = new List<Order>
            {
                new Order() { Id = 2, UserId = 1, EventSeatId = 6, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
            };

            // Act
            var actual = this.orderService.GetCartOrdersById(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetCartOrdersByName_WhenGetCartOrderWithExistingUserName_ShouldBeReturnAllUserOrdersInBasket()
        {
            // Arrange
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const string userName = "admin";

            var expected = new List<Order>
            {
                new Order() { Id = 2, UserId = 1, EventSeatId = 6, DateUtc = this.dateTimeNowMinus20Minutes },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, DateUtc = this.dateTimeNowMinus20Minutes },
            };

            // Act
            var actual = this.orderService.GetCartOrdersByName(userName);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetHistoryOrdersById_WheGetHistoryOrdersWithExistingUserId_ShouldBeReturnAllOrdersWithSoldSeats()
        {
            // Arrange
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const int userId = 1;

            var expected = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
            };

            // Act
            var actual = this.orderService.GetHistoryOrdersById(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetHistoryOrdersById_WheGetHistoryOrdersWithExistingUserName_ShouldBeReturnAllOrdersWithSoldSeats()
        {
            // Arrange
            this.orderService = this.Mock.Create<OrderService>(new NamedParameter("seatLockTimeMinutes", 1));
            const string userName = "admin";

            var expected = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, DateUtc = new DateTime(2017, 12, 12, 12, 00, 00) },
            };

            // Act
            var actual = this.orderService.GetHistoryOrdersByName(userName);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
