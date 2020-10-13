// ****************************************************************************
// <copyright file="LayoutServiceTests.cs" company="EPAM Systems">
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
    internal class LayoutServiceTests : Test
    {
        private ILayoutService layoutService;
        private IRepository<Layout> layoutRepository;

        [SetUp]
        public void Init()
        {
            this.layoutService = this.Container.Resolve<ILayoutService>();
            this.layoutRepository = this.Container.Resolve<IRepository<Layout>>();
        }

        [Test]
        public void AddLayout_WhenAddNewLayout_ShouldBeSaveNewLayoutInRepository()
        {
            // Arrange
            var addLayout = new Layout
            {
                Description = "2",
                VenueId = 2,
                Name = "2",
            };
            var expected = new List<Layout>
            {
                new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 },
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
                addLayout,
            };

            // Act
            this.layoutService.AddLayout(addLayout);

            // Assert
            this.layoutRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateLayout_WhenUpdateLayoutWithExistingId_ShouldBeUpdateAllFieldsInTheRepository()
        {
            // Arrange
            var expected = new Layout
            {
                Id = 2,
                Description = "2",
                VenueId = 2,
                Name = "2",
            };

            // Act
            this.layoutService.UpdateLayout(expected);

            // Assert
            this.layoutRepository.GetById(expected.Id).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteLayout_WhenDeleteLayoutWithExistingLayoutId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            const int existingLayoutId = 1;
            var expected = new List<Layout>
            {
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
            };

            // Act
            this.layoutService.DeleteLayout(existingLayoutId);

            // Assert
            this.layoutRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetLayout_WhenGetLayoutWithExistingLayoutId_ShouldBeReturnThisLayout()
        {
            // Arrange
            const int existingLayoutId = 1;
            var expected = new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 };

            // Act
            var actual = this.layoutService.GetLayout(existingLayoutId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEvents_WhenGetLayouts_ShouldBeReturnAllLayouts()
        {
            // Arrange
            var expected = new List<Layout>
            {
                new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 },
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
            };

            // Act
            var actual = this.layoutService.GetLayouts();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
