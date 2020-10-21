using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TicketManagement.BLL.Infrastructure.Helpers;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.BllServicesTests
{
    [TestFixture]
    internal class SeatLockerTests
    {
        private ISeatLocker seatLocker;

        private IRepository<Order> orderRepository;

        private IRepository<EventSeat> eventSeatRepository;

        private AutoMock Mock { get; set; }

        [SetUp]
        public void Init()
        {
            var orders = new List<Order>
            {
                new Order() {Id = 1, UserId = 1, EventSeatId = 5, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 2, UserId = 1, EventSeatId = 6, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 2, UserId = 1, EventSeatId = 9, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 3, UserId = 2, EventSeatId = 7, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 4, UserId = 2, EventSeatId = 8, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
            };
            var fakeOrderRepository = new RepositoryFake<Order>(orders);

            var eventSeats = new List<EventSeat>
            {
                new EventSeat() {Id = 1, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1},
                new EventSeat() {Id = 2, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 2},
                new EventSeat() {Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3},
                new EventSeat() {Id = 4, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 4},
                new EventSeat() {Id = 5, State = EventSeatState.Sold, EventAreaId = 1, Row = 1, Number = 5},
                new EventSeat() {Id = 6, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 1},
                new EventSeat() {Id = 7, State = EventSeatState.Sold, EventAreaId = 2, Row = 1, Number = 2},
                new EventSeat() {Id = 8, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 3},
                new EventSeat() {Id = 9, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 4},
                new EventSeat() {Id = 10, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 5},
                new EventSeat() {Id = 11, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 1},
                new EventSeat() {Id = 12, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 2},
                new EventSeat() {Id = 13, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 3},
                new EventSeat() {Id = 14, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 4},
                new EventSeat() {Id = 15, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 5},
                new EventSeat() {Id = 16, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 1},
                new EventSeat() {Id = 17, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 2},
                new EventSeat() {Id = 18, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 3},
                new EventSeat() {Id = 19, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 4},
                new EventSeat() {Id = 20, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 5},
            };
            var fakeEventSeatRepository = new RepositoryFake<EventSeat>(eventSeats);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeOrderRepository)
                    .As<IRepository<Order>>();
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
        public void UnlockSeat_WhenUnlockSeatWithExistingOrderId_ShouldBeUpdateEventSeatFromInBasketStatusToFreeStatusAndDeleteThisOrder()
        {
            // Arrange
            this.orderRepository = this.Mock.Create<IRepository<Order>>();
            this.eventSeatRepository = this.Mock.Create<IRepository<EventSeat>>();
            this.seatLocker = this.Mock.Create<SeatLocker>();
            const int existingOrderId = 2;
            var expectedEventSeats = new List<EventSeat>
            {
                new EventSeat() {Id = 1, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 1},
                new EventSeat() {Id = 2, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 2},
                new EventSeat() {Id = 3, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 3},
                new EventSeat() {Id = 4, State = EventSeatState.Free, EventAreaId = 1, Row = 1, Number = 4},
                new EventSeat() {Id = 5, State = EventSeatState.Sold, EventAreaId = 1, Row = 1, Number = 5},
                new EventSeat() {Id = 6, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 1},
                new EventSeat() {Id = 7, State = EventSeatState.Sold, EventAreaId = 2, Row = 1, Number = 2},
                new EventSeat() {Id = 8, State = EventSeatState.InBasket, EventAreaId = 2, Row = 1, Number = 3},
                new EventSeat() {Id = 9, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 4},
                new EventSeat() {Id = 10, State = EventSeatState.Free, EventAreaId = 2, Row = 1, Number = 5},
                new EventSeat() {Id = 11, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 1},
                new EventSeat() {Id = 12, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 2},
                new EventSeat() {Id = 13, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 3},
                new EventSeat() {Id = 14, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 4},
                new EventSeat() {Id = 15, State = EventSeatState.Free, EventAreaId = 3, Row = 1, Number = 5},
                new EventSeat() {Id = 16, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 1},
                new EventSeat() {Id = 17, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 2},
                new EventSeat() {Id = 18, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 3},
                new EventSeat() {Id = 19, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 4},
                new EventSeat() {Id = 20, State = EventSeatState.Free, EventAreaId = 4, Row = 1, Number = 5},
            };

            var expectedOrders = new List<Order>
            {
                new Order() {Id = 1, UserId = 1, EventSeatId = 5, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 2, UserId = 1, EventSeatId = 9, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 3, UserId = 2, EventSeatId = 7, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
                new Order() {Id = 4, UserId = 2, EventSeatId = 8, Date = new DateTime(2017, 12, 12, 12, 00, 00) },
            };

            // Act
            this.seatLocker.UnlockSeat(existingOrderId);

            // Assert
            using (new AssertionScope())
            {
                this.eventSeatRepository.GetAll().Should().BeEquivalentTo(expectedEventSeats);
                this.orderRepository.GetAll().Should().BeEquivalentTo(expectedOrders);
            }
        }
    }
}
