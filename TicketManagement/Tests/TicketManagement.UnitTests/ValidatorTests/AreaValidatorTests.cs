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
                new Area() { Id = 1, Description = "First area of first layout", CoordX = 1, CoordY = 1, LayoutId = 1 },
                new Area() { Id = 2, Description = "Second area of first layout", CoordX = 2, CoordY = 2, LayoutId = 1 },
                new Area() { Id = 3, Description = "First area of second layout", CoordX = 3, CoordY = 3, LayoutId = 2 },
                new Area() { Id = 4, Description = "Second area of second layout", CoordX = 4, CoordY = 4, LayoutId = 2 },
                new Area() { Id = 5, Description = "First area of third layout", CoordX = 1, CoordY = 1, LayoutId = 3 },
                new Area() { Id = 6, Description = "Second area of third layout", CoordX = 2, CoordY = 2, LayoutId = 3 },
                new Area() { Id = 7, Description = "First area of fourth layout", CoordX = 3, CoordY = 3, LayoutId = 4 },
                new Area() { Id = 8, Description = "Second area of fourth layout", CoordX = 4, CoordY = 4, LayoutId = 4 },
                new Area() { Id = 101, Description = "Second area of fourth layout", CoordX = 4, CoordY = 4, LayoutId = 101 },
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
                .As<IRepository<Area, int>>();
                builder.RegisterInstance(fakeLayoutRepository)
                .As<IRepository<Layout, int>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddArea_NonexistentVenue_GetException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var nonexistingLayoutId = 100000;
            var dto = this.Fixture.Build<Area>().With(e => e.LayoutId, nonexistingLayoutId).Create();

            // Act
            Action validate = () => this.areaValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void AddArea_IsSeatRowAndNumber_GetException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var dto = this.Fixture.Build<Area>()
                .With(e => e.LayoutId, 1)
                .With(e => e.Description, "Second area of first layout").Create();

            Action validate = () => this.areaValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().Throw<AreaWithSameDescriptionInTheLayoutExistException>();
        }

        [Test]
        public void AddArea_IsSeatRowAndNumber_NotThrowException()
        {
            // Arrange
            this.areaValidator = this.Mock.Create<AreaValidator>();
            var dto = this.Fixture.Build<Area>()
                 .With(e => e.LayoutId, 1)
                 .With(e => e.Description, "2. Second area of first layout").Create();

            Action validate = () => this.areaValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().NotThrow<AreaWithSameDescriptionInTheLayoutExistException>();
        }
    }
}
