// ****************************************************************************
// <copyright file="SeatServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class SeatServiceTests : Test
    {
        private ISeatService seatService;
        private IRepository<Seat> seatRepository;

        [SetUp]
        public void Init()
        {
            this.seatService = this.Container.Resolve<ISeatService>();
            this.seatRepository = this.Container.Resolve<IRepository<Seat>>();
        }

        [Test]
        public void AddSeat_WhenAddNewSeat_ShouldBeSaveNewSeatInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            var seatDto = new Seat
            {
                Row = 2,
                Number = 2,
                AreaId = 2,
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
                new Seat() { Id = 30, AreaId = 1, Number = 6, Row = 2 },
                new Seat() { Id = 31, AreaId = 1, Number = 7, Row = 2 },
                new Seat() { Id = 32, AreaId = 1, Number = 8, Row = 2 },
                new Seat() { Id = 33, AreaId = 1, Number = 9, Row = 2 },
                new Seat() { Id = 34, AreaId = 1, Number = 10, Row = 2 },
                new Seat() { Id = 35, AreaId = 1, Number = 11, Row = 3 },
                new Seat() { Id = 36, AreaId = 1, Number = 12, Row = 3 },
                new Seat() { Id = 37, AreaId = 1, Number = 13, Row = 3 },
                new Seat() { Id = 38, AreaId = 1, Number = 14, Row = 3 },
                new Seat() { Id = 39, AreaId = 1, Number = 15, Row = 3 },
                new Seat() { Id = 4007, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 4008, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 4009, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 4010, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 4011, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 4012, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 4014, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 4015, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 4016, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 4017, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 4018, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 4019, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 4020, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 4021, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 4022, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 4023, AreaId = 8, Number = 5, Row = 1 },
                seatDto,
            };

            // Act
            this.seatService.AddSeat(seatDto);

            // Assert
            this.seatRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateSeat_WhenUpdateSeatWithExistingId_ShouldBeUpdateAllFieldInTheRepository()
        {
            // Arrange
            var expected = new Seat
            {
                Id = 2,
                Row = 2,
                Number = 2,
                AreaId = 2,
            };

            // Act
            this.seatService.UpdateSeat(expected);

            // Assert
            this.seatRepository.GetById(expected.Id).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteSeat_WhenDeleteSeatWithExistingSeatId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            const int existingSeatId = 1;
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
                new Seat() { Id = 30, AreaId = 1, Number = 6, Row = 2 },
                new Seat() { Id = 31, AreaId = 1, Number = 7, Row = 2 },
                new Seat() { Id = 32, AreaId = 1, Number = 8, Row = 2 },
                new Seat() { Id = 33, AreaId = 1, Number = 9, Row = 2 },
                new Seat() { Id = 34, AreaId = 1, Number = 10, Row = 2 },
                new Seat() { Id = 35, AreaId = 1, Number = 11, Row = 3 },
                new Seat() { Id = 36, AreaId = 1, Number = 12, Row = 3 },
                new Seat() { Id = 37, AreaId = 1, Number = 13, Row = 3 },
                new Seat() { Id = 38, AreaId = 1, Number = 14, Row = 3 },
                new Seat() { Id = 39, AreaId = 1, Number = 15, Row = 3 },
                new Seat() { Id = 4007, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 4008, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 4009, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 4010, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 4011, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 4012, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 4014, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 4015, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 4016, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 4017, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 4018, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 4019, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 4020, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 4021, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 4022, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 4023, AreaId = 8, Number = 5, Row = 1 },
            };

            // Act
            this.seatService.DeleteSeat(existingSeatId);

            // Assert
            this.seatRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetSeat_WhenGetSeatWithExistingSeatId_ShouldBeReturnThisSeat()
        {
            // Arrange
            const int existingSeatId = 1;
            var expectedDto = new Seat() { Id = existingSeatId, AreaId = 1, Number = 1, Row = 1 };

            // Act
            var actualDto = this.seatService.GetSeat(existingSeatId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetSeats_WhenGetSeats_ShouldBeReturnAllSeats()
        {
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
                new Seat() { Id = 30, AreaId = 1, Number = 6, Row = 2 },
                new Seat() { Id = 31, AreaId = 1, Number = 7, Row = 2 },
                new Seat() { Id = 32, AreaId = 1, Number = 8, Row = 2 },
                new Seat() { Id = 33, AreaId = 1, Number = 9, Row = 2 },
                new Seat() { Id = 34, AreaId = 1, Number = 10, Row = 2 },
                new Seat() { Id = 35, AreaId = 1, Number = 11, Row = 3 },
                new Seat() { Id = 36, AreaId = 1, Number = 12, Row = 3 },
                new Seat() { Id = 37, AreaId = 1, Number = 13, Row = 3 },
                new Seat() { Id = 38, AreaId = 1, Number = 14, Row = 3 },
                new Seat() { Id = 39, AreaId = 1, Number = 15, Row = 3 },
                new Seat() { Id = 4007, AreaId = 5, Number = 5, Row = 1 },
                new Seat() { Id = 4008, AreaId = 6, Number = 1, Row = 1 },
                new Seat() { Id = 4009, AreaId = 6, Number = 2, Row = 1 },
                new Seat() { Id = 4010, AreaId = 6, Number = 3, Row = 1 },
                new Seat() { Id = 4011, AreaId = 6, Number = 4, Row = 1 },
                new Seat() { Id = 4012, AreaId = 6, Number = 5, Row = 1 },
                new Seat() { Id = 4014, AreaId = 7, Number = 1, Row = 1 },
                new Seat() { Id = 4015, AreaId = 7, Number = 2, Row = 1 },
                new Seat() { Id = 4016, AreaId = 7, Number = 3, Row = 1 },
                new Seat() { Id = 4017, AreaId = 7, Number = 4, Row = 1 },
                new Seat() { Id = 4018, AreaId = 7, Number = 5, Row = 1 },
                new Seat() { Id = 4019, AreaId = 8, Number = 1, Row = 1 },
                new Seat() { Id = 4020, AreaId = 8, Number = 2, Row = 1 },
                new Seat() { Id = 4021, AreaId = 8, Number = 3, Row = 1 },
                new Seat() { Id = 4022, AreaId = 8, Number = 4, Row = 1 },
                new Seat() { Id = 4023, AreaId = 8, Number = 5, Row = 1 },
            };

            // Act
            var actual = this.seatService.GetSeats();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
