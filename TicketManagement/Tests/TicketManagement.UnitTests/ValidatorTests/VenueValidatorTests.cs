// ****************************************************************************
// <copyright file="VenueValidatorTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Exceptions.VenueExceptions;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ValidatorTests
{
    [TestFixture]
    internal class VenueValidatorTests
    {
        private IVenueValidator venueValidator;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var venues = new List<Venue>
            {
                new Venue() { Id = 1, Description = "First venue", Name = "first", Address = "First venue address", Phone = "123 45 678 90 12" },
                new Venue() { Id = 2, Description = "Second venue", Name = "second", Address = "Second venue address", Phone = "123 45 678 90 12" },
            };
            var fakeVenueRepository = new RepositoryFake<Venue>(venues);

            this.Mock = AutoMock.GetLoose(builder =>
            {
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
        public void Validation_WhenValidationVenueWithNotExistingVenueName_ShouldNotBeThrowException()
        {
            // Arrange
            this.venueValidator = this.Mock.Create<VenueValidator>();
            const string existingName = "third";
            var dto = this.Fixture.Build<Venue>()
                .With(e => e.Name, existingName)
                .Create();

            // Act
            Action validate = () => this.venueValidator.Validation(dto);

            // Assert
            validate.Should().NotThrow();
        }

        [Test]
        public void Validation_WhenValidationVenueWithExistingVenueName_ShouldBeThrowExceptionVenueWithThisNameExistException()
        {
            // Arrange
            this.venueValidator = this.Mock.Create<VenueValidator>();
            const string existingName = "first";
            var dto = this.Fixture.Build<Venue>()
                .With(e => e.Name, existingName)
                .Create();

            // Act
            Action validate = () => this.venueValidator.Validation(dto);

            // Assert
            validate.Should().Throw<VenueWithThisNameExistException>();
        }
    }
}
