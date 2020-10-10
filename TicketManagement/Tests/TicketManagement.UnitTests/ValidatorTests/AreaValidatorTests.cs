// ****************************************************************************
// <copyright file="AreaValidatorTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Exceptions.AreaExceptions;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ValidatorTests
{
    [TestFixture]
    internal class AreaValidatorTests
    {
        private IAreaValidator areaValidator;

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

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
            var fakeAreasRepository = new RepositoryFake<Area>(areas);
            var layouts = new List<Layout>
            {
                new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 },
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 100, Name = "forth2", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 101, Name = "forth1", Description = "Second layout", VenueId = 2 },
            };
            var fakeLayoutRepository = new RepositoryFake<Layout>(layouts);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeAreasRepository)
                .As<IRepository<Area>>();
                builder.RegisterInstance(fakeLayoutRepository)
                .As<IRepository<Layout>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Validation_WhenValidationAreaWithNonexistentVenue_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var nonexistingLayoutId = 100000;
            var dto = this.Fixture.Build<Area>().With(e => e.LayoutId, nonexistingLayoutId).Create();

            // Act
            Action validate = () => this.areaValidator.Validation(dto);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void Validation_WhenValidationAreaWithExistingDescriptionInThisLayout_ShouldBeThrowAreaWithSameDescriptionInTheLayoutExistException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var dto = this.Fixture.Build<Area>()
                .With(e => e.LayoutId, 1)
                .With(e => e.Description, "Second area of first layout").Create();

            // Act
            Action validate = () => this.areaValidator.Validation(dto);

            // Assert
            validate.Should().Throw<AreaWithSameDescriptionInTheLayoutExistException>();
        }

        [Test]
        public void Validation_WhenValidationValidArea_ShouldNotBeThrowException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var dto = this.Fixture.Build<Area>()
                 .With(e => e.LayoutId, 1)
                 .With(e => e.Description, "2. Second area of first layout").Create();

            // Act
            Action validate = () => this.areaValidator.Validation(dto);

            // Assert
            validate.Should().NotThrow();
        }
    }
}
