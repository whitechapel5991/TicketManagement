using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.OrderExceptions;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Repositories.Identity;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ValidatorTests
{
    [TestFixture]
    internal class OrderValidatorTests
    {
        private IOrderValidator orderValidator;

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
                new Order() { Id = 1, UserId = 1, EventSeatId = 5, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = 6, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 2, UserId = 1, EventSeatId = 9, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 3, UserId = 2, EventSeatId = 7, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() { Id = 4, UserId = 2, EventSeatId = 8, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
            };
            var fakeOrderRepository = new RepositoryFake<Order>(orders);

            var users = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 1, UserName = "admin", Email = "admin@admin.com", PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Admin", Surname = "Admin", Balance = 100000, SecurityStamp = "SomeSecureStamp" },
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 50 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
            };
            var fakeUserRepository = new UserRepositoryFake(users);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeEventSeatRepository)
                    .As<IRepository<EventSeat>>();
                builder.RegisterInstance(fakeOrderRepository)
                    .As<IRepository<Order>>();
                builder.RegisterInstance(fakeUserRepository)
                    .As<IUserRepository>();
                builder.RegisterInstance(fakeEventAreaRepository)
                    .As<IRepository<EventArea>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddToCartValidation_WhenAddToCartValidationWithExistingUserAndExistingEventSeatWithFreeStatus_ShouldNotBeThrowException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            var dto = new Order
            {
                EventSeatId = 1,
                UserId = 1,
            };

            // Act
            Action validate = () => this.orderValidator.AddToCartValidation(dto);

            // Assert
            validate.Should().NotThrow();
        }

        [Test]
        public void BuyValidation_WhenBuyValidationWithExistingOrderIdAndExistingEventSeatWithInBasketStatus_ShouldNotBeThrowException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 2;

            // Act
            Action validate = () => this.orderValidator.BuyValidation(orderId);

            // Assert
            validate.Should().NotThrow();
        }

        [Test]
        public void DeleteFromCartValidation_WhenDeleteFromCartValidationWithExistingOrderIdAndExistingEventSeatWithInBasketStatus_ShouldNotBeThrowException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 2;

            // Act
            Action validate = () => this.orderValidator.DeleteFromCartValidation(orderId);

            // Assert
            validate.Should().NotThrow();
        }

        [Test]
        public void AddToCartValidation_WhenAddToCartValidationWithNonexistentEventSeat_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            var dto = new Order
            {
                EventSeatId = 666,
                UserId = 1,
            };

            // Act
            Action validate = () => this.orderValidator.AddToCartValidation(dto);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void AddToCartValidation_WhenAddToCartValidationWithExistingEventSeatAndNonexistentUser_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            var dto = new Order
            {
                EventSeatId = 1,
                UserId = 666,
            };

            // Act
            Action validate = () => this.orderValidator.AddToCartValidation(dto);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void AddToCartValidation_WhenAddToCartValidationWithExistingUserAndExistingEventSeatWithNotFreeStatus_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            var dto = new Order
            {
                EventSeatId = 6,
                UserId = 1,
            };

            // Act
            Action validate = () => this.orderValidator.AddToCartValidation(dto);

            // Assert
            validate.Should().Throw<SeatCurrentlySoldOrBlockedException>();
        }

        [Test]
        public void BuyValidation_WhenBuyValidationWithNonexistentOrder_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 666;

            // Act
            Action validate = () => this.orderValidator.BuyValidation(orderId);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void BuyValidation_WhenBuyValidationWithExistentOrderButSeatHasNotInBasketState_ShouldBeThrowSeatIsNotInTheBasketException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 1;

            // Act
            Action validate = () => this.orderValidator.BuyValidation(orderId);

            // Assert
            validate.Should().Throw<SeatIsNotInTheBasketException>();
        }

        [Test]
        public void DeleteFromCartValidation_WhenDeleteFromCartValidationWithNonexistentOrder_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 666;

            // Act
            Action validate = () => this.orderValidator.DeleteFromCartValidation(orderId);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void DeleteFromCartValidation_WhenDeleteFromCartValidationWithExistentOrderButSeatHasNotInBasketState_ShouldBeThrowSeatIsNotInTheBasketException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 1;

            // Act
            Action validate = () => this.orderValidator.DeleteFromCartValidation(orderId);

            // Assert
            validate.Should().Throw<SeatIsNotInTheBasketException>();
        }

        [Test]
        public void BuyValidation_WhenBuyValidationWithExistentOrderWithSeatInBasketStateButUserDoesNotHaveEnoughMoney_ShouldBeThrowSeatIsNotInTheBasketException()
        {
            // Arrange
            this.orderValidator = this.Mock.Create<OrderValidator>();
            const int orderId = 4;

            // Act
            Action validate = () => this.orderValidator.BuyValidation(orderId);

            // Assert
            validate.Should().Throw<NotEnoughMoneyException>();
        }
    }
}
