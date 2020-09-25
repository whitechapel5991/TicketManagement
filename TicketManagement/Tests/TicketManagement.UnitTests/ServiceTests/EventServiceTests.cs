// ****************************************************************************
// <copyright file="EventServiceTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class EventServiceTests
    {
        private IEventService eventService;

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var events = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
            };
            var fakeVenueRepository = new RepositoryFake<Event>(events);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeVenueRepository)
                .As<IRepository<Event>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddEvent_AddNewEvent_GetCountEvents()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var dto = this.Fixture.Build<Event>().Create();
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
                dto,
            };

            // Act
            this.eventService.AddEvent(dto);

            // Assert
            expected.Should().BeEquivalentTo(eventRepository.GetAll());
        }

        [Test]
        public void UpdateEvent_NewEvent_GetEventDescription()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var id = 2;
            var expectedDto = this.Fixture.Build<Event>().With(e => e.Id, id).Create();

            // Act
            this.eventService.UpdateEvent(expectedDto);

            // Assert
            eventRepository.GetById(id).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void PublishEvent_UpdateOnlyPublishFlag_GetPublishEvent()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var id = 1;
            var expectedDto = new Event()
            {
                Id = 1,
                BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
                Published = true,
            };

            // Act
            this.eventService.PublishEvent(id);

            // Assert
            eventRepository.GetById(id).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteEvent_EventId_GeteventsCount()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            int id = 2;
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event",
                },
            };

            // Act
            this.eventService.DeleteEvent(id);

            // Assert
            expected.Should().BeEquivalentTo(eventRepository.GetAll());
        }

        [Test]
        public void GetEvent_EventId_GetEventDescription()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var id = 1;
            var expectedDto = new Event()
            {
                Id = 1,
                BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                EndDate = new DateTime(2025, 12, 12, 13, 00, 00),
                Description = "First",
                LayoutId = 1,
                Name = "First event",
            };

            // Act
            var actualDto = this.eventService.GetEvent(id);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetEvents_GetEventsCount()
        {
            // Arrange
            var eventRepository = this.Mock.Create<IRepository<Event>>();
            this.eventService = this.Mock.Create<EventService>();
            var expected = new List<Event>
            {
                new Event()
                {
                    Id = 1, BeginDate = new DateTime(2025, 12, 12, 12, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 13, 00, 00), Description = "First",
                    LayoutId = 1, Name = "First event", Published = false,
                },
                new Event()
                {
                    Id = 2, BeginDate = new DateTime(2025, 12, 12, 13, 00, 00),
                    EndDate = new DateTime(2025, 12, 12, 14, 00, 00), Description = "Second",
                    LayoutId = 2, Name = "Second event", Published = true,
                },
            };

            // Act
            var actual = this.eventService.GetEvents();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
