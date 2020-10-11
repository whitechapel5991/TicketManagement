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

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

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
        public void Validation_WhenValidationLayoutWithNonexistentVenueIdParameter_ShouldBeThrowEntityDoesNotExistException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            const int nonexistentVenueId = 100000;
            var dto = this.Fixture.Build<Layout>()
                .With(e => e.VenueId, nonexistentVenueId)
                .Create();

            // Act
            Action validate = () => this.layoutValidator.Validation(dto);

            // Assert
            validate.Should().Throw<EntityDoesNotExistException>();
        }

        [Test]
        public void Validation_WhenValidationLayoutWithExistingLayoutNameInTheVenue_ShouldBeThrowLayoutWithSameNameInTheVenueExistException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            const int venueId = 1;
            const string layoutName = "first";
            var dto = this.Fixture.Build<Layout>()
                .With(e => e.VenueId, venueId)
                .With(e => e.Name, layoutName)
                .Create();

            // Act
            Action validate = () => this.layoutValidator.Validation(dto);

            // Assert
            validate.Should().Throw<LayoutWithSameNameInTheVenueExistException>();
        }

        [Test]
        public void Validation_WhenValidationLayoutWithNotExistingNameInVenueAndExistingVenueId_ShouldNotBeThrowException()
        {
            // Arrange
            this.layoutValidator = this.Mock.Create<LayoutValidator>();
            var dto = this.Fixture.Build<Layout>()
                .With(e => e.VenueId, 1)
                .With(e => e.Name, "third").Create();

            // Act
            Action validate = () => this.layoutValidator.Validation(dto);

            // Assert
            validate.Should().NotThrow();
        }
    }
}
