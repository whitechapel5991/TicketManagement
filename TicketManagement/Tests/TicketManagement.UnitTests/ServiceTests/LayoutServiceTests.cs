// ****************************************************************************
// <copyright file="LayoutServiceTests.cs" company="EPAM Systems">
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
    internal class LayoutServiceTests
    {
        private ILayoutService layoutService;

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

            this.Mock = AutoMock.GetLoose(builder =>
            {
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
        public void AddLayout_AddNewLayout_GetCountLayouts()
        {
            // Arrange
            var layoutRepository = this.Mock.Create<IRepository<Layout, int>>();
            this.layoutService = this.Mock.Create<LayoutService>();
            var dto = this.Fixture.Build<Layout>().Create();
            var expected = new List<Layout>
            {
                new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 },
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 100, Name = "forth2", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 101, Name = "forth1", Description = "Second layout", VenueId = 2 },
                dto,
            };

            // Act
            this.layoutService.AddLayout(dto);

            // Assert
            expected.Should().BeEquivalentTo(layoutRepository.GetAll());
        }

        [Test]
        public void UpdateLayout_NewLayout_GetLayoutDescription()
        {
            // Arrange
            var layoutRepository = this.Mock.Create<IRepository<Layout, int>>();
            this.layoutService = this.Mock.Create<LayoutService>();
            var id = 1;
            var expectedDto = this.Fixture.Build<Layout>()
                .With(e => e.Id, id)
                .Create();

            // Act
            this.layoutService.UpdateLayout(expectedDto);

            // Assert
            layoutRepository.GetById(id).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteLayout_LayoutId_GetLayoutsCount()
        {
            // Arrange
            var layoutRepository = this.Mock.Create<IRepository<Layout, int>>();
            this.layoutService = this.Mock.Create<LayoutService>();
            int id = 1;
            var expected = new List<Layout>
            {
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 100, Name = "forth2", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 101, Name = "forth1", Description = "Second layout", VenueId = 2 },
            };

            // Act
            this.layoutService.DeleteLayout(id);

            // Assert
            expected.Should().BeEquivalentTo(layoutRepository.GetAll());
        }

        [Test]
        public void GetLayout_LayoutId_GetLayoutDescription()
        {
            // Arrange
            var layoutRepository = this.Mock.Create<IRepository<Layout, int>>();
            this.layoutService = this.Mock.Create<LayoutService>();
            var id = 1;
            var expectedDto = new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 };

            // Act
            var actualDto = this.layoutService.GetLayout(id);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEvents_GetEventsCount()
        {
            // Arrange
            var layoutRepository = this.Mock.Create<IRepository<Layout, int>>();
            this.layoutService = this.Mock.Create<LayoutService>();
            var expected = new List<Layout>
            {
                new Layout() { Id = 1, Name = "first", Description = "First layout", VenueId = 1 },
                new Layout() { Id = 2, Name = "second", Description = "Second layout", VenueId = 1 },
                new Layout() { Id = 3, Name = "third", Description = "First layout", VenueId = 2 },
                new Layout() { Id = 4, Name = "forth", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 100, Name = "forth2", Description = "Second layout", VenueId = 2 },
                new Layout() { Id = 101, Name = "forth1", Description = "Second layout", VenueId = 2 },
            };

            // Act
            var actual = this.layoutService.GetLayouts();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
