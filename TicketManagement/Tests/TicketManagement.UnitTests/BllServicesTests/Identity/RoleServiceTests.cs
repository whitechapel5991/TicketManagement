// ****************************************************************************
// <copyright file="RoleServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.BLL.Services.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.BllServicesTests.Identity
{
    [TestFixture]
    internal class RoleServiceTests
    {
        private IRoleService roleService;

        private IRoleRepository roleRepository;

        private AutoMock Mock { get; set; }

        [SetUp]
        public void Init()
        {
            var roles = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
            };

            var fakeRoleRepository = new RoleRepositoryFake(roles);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeRoleRepository)
                    .As<IRoleRepository>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Add_WhenAddNewRole_ShouldSaveNewDataInRepository()
        {
            // Arrange
            this.roleRepository = this.Mock.Create<IRoleRepository>();
            this.roleService = this.Mock.Create<RoleService>();
            const string role = "newRole";
            var expected = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
                new Role() { Name = role },
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
            this.roleRepository = this.Mock.Create<IRoleRepository>();
            this.roleService = this.Mock.Create<RoleService>();
            const string role = "admin";
            var expected = new List<Role>
            {
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
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
            this.roleService = this.Mock.Create<RoleService>();
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
            this.roleService = this.Mock.Create<RoleService>();
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
            this.roleRepository = this.Mock.Create<IRoleRepository>();
            this.roleService = this.Mock.Create<RoleService>();
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
            this.roleService = this.Mock.Create<RoleService>();
            var expected = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
            };

            // Act
            var actual = this.roleService.GetAll();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
