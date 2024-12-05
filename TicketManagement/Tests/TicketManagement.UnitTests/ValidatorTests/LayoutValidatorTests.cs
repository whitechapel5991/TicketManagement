// ****************************************************************************
// <copyright file="LayoutValidatorTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Exceptions.LayoutExceptions;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ValidatorTests
{
    [TestFixture]
    internal class LayoutValidatorTests
    {
        private ILayoutValidator layoutValidator;

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

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
            var venues = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };
            var fakeVenueRepository = new RepositoryFake<Venue>(venues);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeLayoutRepository)
                .As<IRepository<Layout>>();
                builder.RegisterInstance(fakeVenueRepository)
                .As<IRepository<Venue>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddLayout_NonexistentVenue_GetException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            var nonexistingVenueId = 100000;
            var dto = this.Fixture.Build<Layout>().With(e => e.VenueId, nonexistingVenueId).Create();

            // Act
            Action validate = () => this.layoutValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void AddLayout_IsSeatRowAndNumber_GetException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            var dto = this.Fixture.Build<Layout>()
                .With(e => e.VenueId, 1)
                .With(e => e.Name, "first").Create();

            Action validate = () => this.layoutValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().Throw<LayoutWithSameNameInTheVenueExistException>();
        }

        [Test]
        public void AddLayout_IsSeatRowAndNumber_NotThrowException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            var dto = this.Fixture.Build<Layout>()
    .With(e => e.VenueId, 1)
    .With(e => e.Name, "third").Create();

            Action validate = () => this.layoutValidator.Validate(dto);

            // Act - delegate. Assert
            validate.Should().NotThrow<LayoutWithSameNameInTheVenueExistException>();
        }
    }
}
