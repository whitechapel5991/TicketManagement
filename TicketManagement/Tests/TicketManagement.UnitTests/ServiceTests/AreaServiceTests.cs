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
        public void AddArea_AddNewArea_GetCountAreas()
        {
            // Arrange
            var areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            var dto = this.Fixture.Build<Area>().Create();
            var expected = new List<Area>
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
                dto,
            };

            // Act
            this.areaService.AddArea(dto);

            // Assert
            expected.Should().BeEquivalentTo(areaRepository.GetAll());
        }

        [Test]
        public void UpdateArea_NewArea_GetAreaDescription()
        {
            // Arrange
            var areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            var id = 1;
            var expectedDto = this.Fixture.Build<Area>()
                .With(e => e.Id, id)
                .Create();

            // Act
            this.areaService.UpdateArea(expectedDto);

            // Assert
            areaRepository.GetById(id).Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void DeleteArea_AreaId_GetAreasCount()
        {
            // Arrange
            var areaRepository = this.Mock.Create<IRepository<Area>>();
            this.areaService = this.Mock.Create<AreaService>();
            int id = 1;
            var expected = new List<Area>
            {
                new Area() { Id = 2, Description = "Second area of first layout", CoordX = 2, CoordY = 2, LayoutId = 1 },
                new Area() { Id = 3, Description = "First area of second layout", CoordX = 3, CoordY = 3, LayoutId = 2 },
                new Area() { Id = 4, Description = "Second area of second layout", CoordX = 4, CoordY = 4, LayoutId = 2 },
                new Area() { Id = 5, Description = "First area of third layout", CoordX = 1, CoordY = 1, LayoutId = 3 },
                new Area() { Id = 6, Description = "Second area of third layout", CoordX = 2, CoordY = 2, LayoutId = 3 },
                new Area() { Id = 7, Description = "First area of fourth layout", CoordX = 3, CoordY = 3, LayoutId = 4 },
                new Area() { Id = 8, Description = "Second area of fourth layout", CoordX = 4, CoordY = 4, LayoutId = 4 },
                new Area() { Id = 101, Description = "Second area of fourth layout", CoordX = 4, CoordY = 4, LayoutId = 101 },
            };

            // Act
            this.areaService.DeleteArea(id);

            // Assert
            expected.Should().BeEquivalentTo(areaRepository.GetAll());
        }

        [Test]
        public void GetArea_AreaId_GetAreaDescription()
        {
            // Arrange
            this.areaService = this.Mock.Create<AreaService>();
            var expectedDto = new Area() { Id = 1, Description = "First area of first layout", CoordX = 1, CoordY = 1, LayoutId = 1 };

            // Act
            var actualDto = this.areaService.GetArea(1);

            // Assert
            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetAreas_GetAreasCount()
        {
            // Arrange
            this.areaService = this.Mock.Create<AreaService>();
            var expected = new List<Area>
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

            // Act
            var actual = this.areaService.GetAreas();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
