// ****************************************************************************
// <copyright file="AreaServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests
{
    [TestFixture]
    internal class AreaServiceTests
    {
        private IAreaService areaService;

        private IRepository<Area> areaRepository;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

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
            var fakeAreaRepository = new RepositoryFake<Area>(areas);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeAreaRepository)
                .As<IRepository<Area>>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void AddArea_WhenAddNewValidArea_ShouldSaveNewDataInRepositoryAndReturnNewEntityId()
        {
            // Arrange
            this.areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            var dto = this.Fixture.Build<Area>().Create();
            var expected = new List<Area>
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
                dto,
            };

            // Act
            var expectedEntityId = this.areaService.AddArea(dto);

            // Assert
            using (new AssertionScope())
            {
                this.areaRepository.GetAll().Should().BeEquivalentTo(expected);
                dto.Id.Should().Be(expectedEntityId);
            }
        }

        [Test]
        public void UpdateArea_WhenUpdateAreaWithExistingId_ShouldBeUpdateAllFieldsInTheRepository()
        {
            // Arrange
            this.areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            const int existingAreaId = 1;
            var expectedDto = this.Fixture.Build<Area>()
                .With(e => e.Id, existingAreaId)
                .Create();

            // Act
            this.areaService.UpdateArea(expectedDto);

            // Assert
            this.areaRepository.GetById(existingAreaId).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteArea_WhenDeleteAreaWithExistingAreaId_ShouldBeDeleteFromTheRepository()
        {
            // Arrange
            this.areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            const int existingAreaId = 1;
            var expected = new List<Area>
            {
                new Area() { Id = 2, Description = "Second area of first layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 1 },
                new Area() { Id = 3, Description = "First area of second layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 2 },
                new Area() { Id = 4, Description = "Second area of second layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 2 },
                new Area() { Id = 5, Description = "First area of third layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 3 },
                new Area() { Id = 6, Description = "Second area of third layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 3 },
                new Area() { Id = 7, Description = "First area of fourth layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 4 },
                new Area() { Id = 8, Description = "Second area of fourth layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 4 },
                new Area() { Id = 101, Description = "Second area of fourth layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 101 },
            };

            // Act
            this.areaService.DeleteArea(existingAreaId);

            // Assert
            this.areaRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetArea_WhenGetAreaWithExistingAreaId_ShouldBeReturnThisArea()
        {
            // Arrange
            this.areaService = this.Mock.Create<AreaService>();
            const int existingAreaId = 1;
            var expectedDto = new Area() { Id = 1, Description = "First area of first layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 1 };

            // Act
            var actualDto = this.areaService.GetArea(existingAreaId);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetAreas_WhenGetAreas_ShouldBeReturnAllAreas()
        {
            // Arrange
            this.areaService = this.Mock.Create<AreaService>();
            var expected = new List<Area>
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

            // Act
            var actual = this.areaService.GetAreas();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
