// ****************************************************************************
// <copyright file="SeatValidatorTests.cs" company="EPAM Systems">
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
using NUnit.Framework;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.SeatExceptions;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ValidatorTests
{
    [TestFixture]
    internal class SeatValidatorTests
    {
        private ISeatValidator seatValidator;

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

            var areas = new List<Area>
            {
                new Area() { Id = 1, Description = "First area of first layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 1 },
                new Area() { Id = 2, Description = "Second area of first layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 1 },
                new Area() { Id = 3, Description = "First area of second layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 2 },
                new Area() { Id = 4, Description = "Second area of second layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 2 },
                new Area() { Id = 5, Description = "First area of third layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 3 },
                new Area() { Id = 6, Description = "Second area of third layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 3 },
                new Area() { Id = 7, Description = "First area of fourth layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 4 },
                new Area() { Id = 8, Description = "Second area of fourth layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 4 },
                new Area() { Id = 101, Description = "Second area of fourth layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 101 },
            };
            var fakeSeatsRepository = new RepositoryFake<Seat>(seats);
            var fakeAreasRepository = new RepositoryFake<Area>(areas);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeSeatsRepository)
                .As<IRepository<Seat>>();
                builder.RegisterInstance(fakeAreasRepository)
                .As<IRepository<Area>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Validation_WhenValidationSeatWithNonexistentAreaId_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatValidator = this.Mock.Create<SeatValidator>();
            var nonexistingAreaId = 100000;
            var dto = new Seat
            {
                Id = 9,
                Row = 9,
                Number = 9,
                AreaId = nonexistingAreaId,
            };

            // Act
            Action validate = () => this.seatValidator.Validation(dto);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void Validation_WhenValidationSeatWithExistingSeatWithRowAndNumberInThisArea_ShouldBeThrowSeatWithSameRowAndNumberInTheAreaExistException()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatValidator = this.Mock.Create<SeatValidator>();
            var dto = new Seat
            {
                Id = 41,
                Row = 1,
                Number = 1,
                AreaId = 1,
            };

            // Act
            Action validate = () => this.seatValidator.Validation(dto);

            // Assert
            validate.Should().Throw<SeatWithSameRowAndNumberInTheAreaExistException>();
        }

        [Test]
        public void Validation_WhenValidationSeatWithNotExistingSeatWithTheSameRowAndNumberInThisArea_ShouldNotBeThrowException()
        {
            // Arrange
            var seatRepository = this.Mock.Create<IRepository<Seat>>();
            this.seatValidator = this.Mock.Create<SeatValidator>();
            var dto = new Seat
            {
                Id = 41,
                Row = 13,
                Number = 13,
                AreaId = 1,
            };

            // Act
            Action validate = () => this.seatValidator.Validation(dto);

            // Assert
            validate.Should().NotThrow();
        }
    }
}
