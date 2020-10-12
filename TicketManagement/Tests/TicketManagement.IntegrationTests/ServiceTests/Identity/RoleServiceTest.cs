// ****************************************************************************
// <copyright file="RoleServiceTest.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests.Identity
{
    [TestFixture]
    internal class RoleServiceTest : Test
    {
        private IRoleService roleService;

        private IRoleRepository roleRepository;

        [SetUp]
        public void Init()
        {
            this.roleService = this.Container.Resolve<IRoleService>();
            this.roleRepository = this.Container.Resolve<IRoleRepository>();
        }

        [Test]
        public void Add_WhenAddNewRole_ShouldSaveNewDataInRepository()
        {
            // Arrange
            const string role = "newRole";
            var expected = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
                new Role() { Id = 4, Name = "venue manager" },
                new Role() { Id = 5, Name = role },
            };

            // Act
            this.roleService.Add(role);

            // Assert
            this.roleRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Delete_WhenDeleteExistingRole_ShouldBeDeleteFromRepository()
        {
            // Arrange
            const string role = "admin";
            var expected = new List<Role>
            {
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
                new Role() { Id = 4, Name = "venue manager" },
            };

            // Act
            this.roleService.Delete(role);

            // Assert
            this.roleRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FindById_WhenFindRoleByExistingId_ShouldBeReturnThisRole()
        {
            // Arrange
            const int roleId = 1;
            var expected = new Role() { Id = roleId, Name = "admin" };

            // Act
            var actual = this.roleService.FindById(roleId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FindByName_WhenFindRoleByExistingName_ShouldBeReturnThisRole()
        {
            // Arrange
            const string roleName = "admin";
            var expected = new Role() { Id = 1, Name = roleName };

            // Act
            var actual = this.roleService.FindByName(roleName);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Update_WhenUpdateExistingRole_ShouldBeSaveChanges()
        {
            // Arrange
            var expected = new Role() { Id = 1, Name = "admin changes" };

            // Act
            this.roleService.Update(expected);

            // Assert
            this.roleRepository.GetById(expected.Id).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAll_WhenGetAllRoles_ShouldBeReturnAllRoles()
        {
            // Arrange
            var expected = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
                new Role() { Id = 4, Name = "venue manager" },
            };

            // Act
            var actual = this.roleService.GetAll();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
