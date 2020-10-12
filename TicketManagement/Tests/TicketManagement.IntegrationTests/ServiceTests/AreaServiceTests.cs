// ****************************************************************************
// <copyright file="AreaServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
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
    internal class AreaServiceTests : Test
    {
        private IAreaService areaService;
        private IRepository<Area> areaRepository;

        [SetUp]
        public void Init()
        {
            this.areaService = this.Container.Resolve<IAreaService>();
            this.areaRepository = this.Container.Resolve<IRepository<Area>>();
        }

        [Test]
        public void AddArea_WhenAddNewArea_ShouldSaveNewDataInRepository()
        {
            // Arrange
            var expected = new Area
            {
                Description = "blab-la5",
                CoordinateX = 1000,
                CoordinateY = 1000,
                LayoutId = 1,
            };

            // Act
            var id = this.areaService.AddArea(expected);

            // Assert
            expected.Should().BeEquivalentTo(this.areaRepository.GetById(id));
        }

        [Test]
        public void UpdateArea_WhenUpdateExistingArea_ShouldBeUpdateAllFields()
        {
            // Arrange
            var expected = new Area
            {
                Id = 1,
                Description = "blameable",
                CoordinateX = 9999,
                CoordinateY = 9999,
                LayoutId = 2,
            };

            // Act
            this.areaService.UpdateArea(expected);

            // Assert
            expected.Should().BeEquivalentTo(this.areaRepository.GetById(expected.Id));
        }

        [Test]
        public void DeleteArea_WhenDeleteAreaWithExistingId_ShouldBeContainWitoutThisArea()
        {
            // Arrange
            const int areaId = 8;
            var expected = new List<Area>
            {
                new Area() { Id = 1, Description = "First area of first layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 1 },
                new Area() { Id = 2, Description = "Second area of first layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 1 },
                new Area() { Id = 3, Description = "First area of second layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 2 },
                new Area() { Id = 4, Description = "Second area of second layout", CoordinateX = 4, CoordinateY = 4, LayoutId = 2 },
                new Area() { Id = 5, Description = "First area of third layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 3 },
                new Area() { Id = 6, Description = "Second area of third layout", CoordinateX = 2, CoordinateY = 2, LayoutId = 3 },
                new Area() { Id = 7, Description = "First area of fourth layout", CoordinateX = 3, CoordinateY = 3, LayoutId = 4 },
            };

            // Act
            this.areaService.DeleteArea(areaId);

            // Assert
            expected.Should().BeEquivalentTo(this.areaRepository.GetAll());
        }

        [Test]
        public void GetArea_WhenGetAreaWithExistingId_ShouldBeReturnThisArea()
        {
            // Arrange
            const int areaId = 1;
            var expected = new Area()
            { Id = 1, Description = "First area of first layout", CoordinateX = 1, CoordinateY = 1, LayoutId = 1 };

            // Act
            var actual = this.areaService.GetArea(areaId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetAreas_WhenGetAllAreas_ShouldBeReturnAllAreas()
        {
            // Arrange
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
            };

            // Act
            var actual = this.areaService.GetAreas();

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }
    }
}
