// ****************************************************************************
// <copyright file="SeatServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class SeatServiceTests
    {
        private ISeatService seatService;

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var seats = new List<Seat>()
            {
                new Seat() { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                new Seat() { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                new Seat() { Id = 3, AreaId = 1, Number = 3, Row = 1 },
                new Seat() { Id = 4, AreaId = 1, Number = 4, Row = 1 },
                new Seat() { Id = 5, AreaId = 1, Number = 5, Row = 1 },
                new Seat() { Id = 6, AreaId = 2, Number = 1, Row = 1 },
                new Seat() { Id = 7, AreaId = 2, Number = 2, Row = 1 },
                new Seat() { Id = 8, AreaId = 2, Number = 3, Row = 1 },
                new Seat() { Id = 9, AreaId = 2, Number = 4, Row = 1 },
                new Seat() { Id = 10, AreaId = 2, Number = 5, Row = 1 },
                new Seat() { Id = 11, AreaId = 3, Number = 1, Row = 1 },
                new Seat() { Id = 12, AreaId = 3, Number = 2, Row = 1 },
                new Seat() { Id = 13, AreaId = 3, Number = 3, Row = 1 },
                new Seat() { Id = 14, AreaId = 3, Number = 4, Row = 1 },
                new Seat() { Id = 15, AreaId = 3, Number = 5, Row = 1 },
                new Seat() { Id = 16, AreaId = 4, Number = 1, Row = 1 },
                new Seat() { Id = 17, AreaId = 4, Number = 2, Row = 1 },
                new Seat() { Id = 18, AreaId = 4, Number = 3, Row = 1 },
                new Seat() { Id = 19, AreaId = 4, Number = 4, Row = 1 },
                new Seat() { Id = 20, AreaId = 4, Number = 5, Row = 1 },
                new Seat() { Id = 21, AreaId = 5, Number = 1, Row = 1 },
                new Seat() { Id = 22, AreaId = 5, Number = 2, Row = 1 },
                new Seat() { Id = 23, AreaId = 5, Number = 3, Row = 1 },
                new Seat() { Id = 24, AreaId = 5, Number = 4, Row = 1 },
                new Seat() { Id = 25, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 26, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 27, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 28, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 29, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 30, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 31, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 32, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 33, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 34, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 35, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 36, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 37, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 38, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 39, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 40, AreaId = 8, Number = 5, Row = 1 },
            };
            var fakeSeatsRepository = new RepositoryFake<Seat>(seats);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeSeatsRepository)
                .As<IRepository<Seat>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddSeat_WhenAddNewSeat_ShouldBeSaveNewSeatInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatService = this.Mock.Create<SeatService>();
            var seatDto = new Seat
            {
                Row = 9,
                Number = 9,
                AreaId = 1,
            };
            var expected = new List<Seat>()
            {
                new Seat() { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                new Seat() { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                new Seat() { Id = 3, AreaId = 1, Number = 3, Row = 1 },
                new Seat() { Id = 4, AreaId = 1, Number = 4, Row = 1 },
                new Seat() { Id = 5, AreaId = 1, Number = 5, Row = 1 },
                new Seat() { Id = 6, AreaId = 2, Number = 1, Row = 1 },
                new Seat() { Id = 7, AreaId = 2, Number = 2, Row = 1 },
                new Seat() { Id = 8, AreaId = 2, Number = 3, Row = 1 },
                new Seat() { Id = 9, AreaId = 2, Number = 4, Row = 1 },
                new Seat() { Id = 10, AreaId = 2, Number = 5, Row = 1 },
                new Seat() { Id = 11, AreaId = 3, Number = 1, Row = 1 },
                new Seat() { Id = 12, AreaId = 3, Number = 2, Row = 1 },
                new Seat() { Id = 13, AreaId = 3, Number = 3, Row = 1 },
                new Seat() { Id = 14, AreaId = 3, Number = 4, Row = 1 },
                new Seat() { Id = 15, AreaId = 3, Number = 5, Row = 1 },
                new Seat() { Id = 16, AreaId = 4, Number = 1, Row = 1 },
                new Seat() { Id = 17, AreaId = 4, Number = 2, Row = 1 },
                new Seat() { Id = 18, AreaId = 4, Number = 3, Row = 1 },
                new Seat() { Id = 19, AreaId = 4, Number = 4, Row = 1 },
                new Seat() { Id = 20, AreaId = 4, Number = 5, Row = 1 },
                new Seat() { Id = 21, AreaId = 5, Number = 1, Row = 1 },
                new Seat() { Id = 22, AreaId = 5, Number = 2, Row = 1 },
                new Seat() { Id = 23, AreaId = 5, Number = 3, Row = 1 },
                new Seat() { Id = 24, AreaId = 5, Number = 4, Row = 1 },
                new Seat() { Id = 25, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 26, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 27, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 28, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 29, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 30, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 31, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 32, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 33, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 34, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 35, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 36, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 37, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 38, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 39, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 40, AreaId = 8, Number = 5, Row = 1 },
                seatDto,
            };

            // Act
            this.seatService.AddSeat(seatDto);

            // Assert
            expected.Should().BeEquivalentTo(seatRepository.GetAll());
        }

        [Test]
        public void UpdateSeat_WhenUpdateSeatWithExistingId_ShouldBeUpdateAllFieldInTheRepository()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatService = this.Mock.Create<SeatService>();
            var expectedDto = new Seat
            {
                Id = 1,
                Row = 9,
                Number = 9,
                AreaId = 1,
            };

            // Act
            this.seatService.UpdateSeat(expectedDto);

            // Assert
            seatRepository.GetById(1).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteSeat_WhenDeleteSeatWithExistingSeatId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatService = this.Mock.Create<SeatService>();
            var id = 1;
            var expected = new List<Seat>()
            {
                new Seat() { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                new Seat() { Id = 3, AreaId = 1, Number = 3, Row = 1 },
                new Seat() { Id = 4, AreaId = 1, Number = 4, Row = 1 },
                new Seat() { Id = 5, AreaId = 1, Number = 5, Row = 1 },
                new Seat() { Id = 6, AreaId = 2, Number = 1, Row = 1 },
                new Seat() { Id = 7, AreaId = 2, Number = 2, Row = 1 },
                new Seat() { Id = 8, AreaId = 2, Number = 3, Row = 1 },
                new Seat() { Id = 9, AreaId = 2, Number = 4, Row = 1 },
                new Seat() { Id = 10, AreaId = 2, Number = 5, Row = 1 },
                new Seat() { Id = 11, AreaId = 3, Number = 1, Row = 1 },
                new Seat() { Id = 12, AreaId = 3, Number = 2, Row = 1 },
                new Seat() { Id = 13, AreaId = 3, Number = 3, Row = 1 },
                new Seat() { Id = 14, AreaId = 3, Number = 4, Row = 1 },
                new Seat() { Id = 15, AreaId = 3, Number = 5, Row = 1 },
                new Seat() { Id = 16, AreaId = 4, Number = 1, Row = 1 },
                new Seat() { Id = 17, AreaId = 4, Number = 2, Row = 1 },
                new Seat() { Id = 18, AreaId = 4, Number = 3, Row = 1 },
                new Seat() { Id = 19, AreaId = 4, Number = 4, Row = 1 },
                new Seat() { Id = 20, AreaId = 4, Number = 5, Row = 1 },
                new Seat() { Id = 21, AreaId = 5, Number = 1, Row = 1 },
                new Seat() { Id = 22, AreaId = 5, Number = 2, Row = 1 },
                new Seat() { Id = 23, AreaId = 5, Number = 3, Row = 1 },
                new Seat() { Id = 24, AreaId = 5, Number = 4, Row = 1 },
                new Seat() { Id = 25, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 26, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 27, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 28, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 29, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 30, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 31, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 32, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 33, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 34, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 35, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 36, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 37, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 38, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 39, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 40, AreaId = 8, Number = 5, Row = 1 },
            };

            // Act
            this.seatService.DeleteSeat(id);

            // Assert
            expected.Should().BeEquivalentTo(seatRepository.GetAll());
        }

        [Test]
        public void GetSeat_WhenGetSeatWithExistingSeatId_ShouldBeReturnThisSeat()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatService = this.Mock.Create<SeatService>();
            var expectedDto = new Seat() { Id = 1, AreaId = 1, Number = 1, Row = 1 };

            // Act
            var actualDto = this.seatService.GetSeat(1);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetSeats_WhenGetSeats_ShouldBeReturnAllSeats()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatService = this.Mock.Create<SeatService>();
            var expected = new List<Seat>()
            {
                new Seat() { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                new Seat() { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                new Seat() { Id = 3, AreaId = 1, Number = 3, Row = 1 },
                new Seat() { Id = 4, AreaId = 1, Number = 4, Row = 1 },
                new Seat() { Id = 5, AreaId = 1, Number = 5, Row = 1 },
                new Seat() { Id = 6, AreaId = 2, Number = 1, Row = 1 },
                new Seat() { Id = 7, AreaId = 2, Number = 2, Row = 1 },
                new Seat() { Id = 8, AreaId = 2, Number = 3, Row = 1 },
                new Seat() { Id = 9, AreaId = 2, Number = 4, Row = 1 },
                new Seat() { Id = 10, AreaId = 2, Number = 5, Row = 1 },
                new Seat() { Id = 11, AreaId = 3, Number = 1, Row = 1 },
                new Seat() { Id = 12, AreaId = 3, Number = 2, Row = 1 },
                new Seat() { Id = 13, AreaId = 3, Number = 3, Row = 1 },
                new Seat() { Id = 14, AreaId = 3, Number = 4, Row = 1 },
                new Seat() { Id = 15, AreaId = 3, Number = 5, Row = 1 },
                new Seat() { Id = 16, AreaId = 4, Number = 1, Row = 1 },
                new Seat() { Id = 17, AreaId = 4, Number = 2, Row = 1 },
                new Seat() { Id = 18, AreaId = 4, Number = 3, Row = 1 },
                new Seat() { Id = 19, AreaId = 4, Number = 4, Row = 1 },
                new Seat() { Id = 20, AreaId = 4, Number = 5, Row = 1 },
                new Seat() { Id = 21, AreaId = 5, Number = 1, Row = 1 },
                new Seat() { Id = 22, AreaId = 5, Number = 2, Row = 1 },
                new Seat() { Id = 23, AreaId = 5, Number = 3, Row = 1 },
                new Seat() { Id = 24, AreaId = 5, Number = 4, Row = 1 },
                new Seat() { Id = 25, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 26, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 27, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 28, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 29, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 30, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 31, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 32, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 33, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 34, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 35, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 36, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 37, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 38, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 39, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 40, AreaId = 8, Number = 5, Row = 1 },
            };

            // Act
            var actual = this.seatService.GetSeats();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
