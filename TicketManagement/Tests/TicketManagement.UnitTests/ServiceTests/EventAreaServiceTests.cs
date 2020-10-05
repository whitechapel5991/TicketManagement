// ****************************************************************************
// <copyright file="EventAreaServiceTests.cs" company="EPAM Systems">
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
    internal class EventAreaServiceTests
    {
        private IEventAreaService eventAreaService;

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var eventAreas = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordX = 1, CoordY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordX = 1, CoordY = 1, Description = "Second area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordX = 2, CoordY = 2, Description = "First area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordX = 2, CoordY = 2, Description = "Second area event", EventId = 2, Price = 200 },
            };
            var fakeEventAreaRepository = new RepositoryFake<EventArea>(eventAreas);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeEventAreaRepository)
                .As<IRepository<EventArea, int>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void UpdateEventArea_NewEventArea_GetEventAreaPrice()
        {
            // Arrange
            var eventAreaRepository = this.Mock.Create<IRepository<EventArea, int>>();
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            var id = 1;
            var expectedDto = this.Fixture.Build<EventArea>()
                .With(e => e.Id, id)
                .Create();

            // Act
            this.eventAreaService.UpdateEventArea(expectedDto);

            // Assert
            eventAreaRepository.GetById(id).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEventArea_EventAreaId_GetEventAreaDescription()
        {
            // Arrange
            var eventAreaRepository = this.Mock.Create<IRepository<EventArea, int>>();
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            var id = 1;
            var expectedDto = new EventArea() { Id = 1, CoordX = 1, CoordY = 1, Description = "First area event", EventId = 1, Price = 100 };

            // Act
            var actualDto = this.eventAreaService.GetEventArea(id);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEventAreas_GetEventAreasCount()
        {
            // Arrange
            var eventAreaRepository = this.Mock.Create<IRepository<EventArea, int>>();
            this.eventAreaService = this.Mock.Create<EventAreaService>();
            var expected = new List<EventArea>
            {
                new EventArea() { Id = 1, CoordX = 1, CoordY = 1, Description = "First area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 2, CoordX = 1, CoordY = 1, Description = "Second area event", EventId = 1, Price = 100 },
                new EventArea() { Id = 3, CoordX = 2, CoordY = 2, Description = "First area event", EventId = 2, Price = 200 },
                new EventArea() { Id = 4, CoordX = 2, CoordY = 2, Description = "Second area event", EventId = 2, Price = 200 },
            };

            // Act
            var actual = this.eventAreaService.GetEventAreas();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
