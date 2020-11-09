// ****************************************************************************
// <copyright file="UserServiceTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.BLL.Services.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.BllServicesTests.Identity
{
    [TestFixture]
    internal class UserServiceTests
    {
        private IUserService userService;

        private IUserRepository userRepository;

        private IUserRoleRepository userRoleRepository;

        private AutoMock Mock { get; set; }

        private Fixture Fixture { get; set; }

        [SetUp]
        public void Init()
        {
            this.Fixture = new Fixture();

            var users = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 1, UserName = "admin", Email = "admin@admin.com", PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Admin", Surname = "Admin", Balance = 100000, SecurityStamp = "SomeSecureStamp" },
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 300 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
            };
            var roles = new List<Role>
            {
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "user" },
                new Role() { Id = 3, Name = "event manager" },
            };
            var userRoles = new List<UserRole>
            {
                new UserRole() { RoleId = 1, UserId = 1 },
                new UserRole() { RoleId = 2, UserId = 2 },
                new UserRole() { RoleId = 3, UserId = 3 },
            };
            var fakeUserRepository = new UserRepositoryFake(users);
            var fakeUserRoleRepository = new UserRoleRepositoryFake(users, userRoles, roles);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeUserRepository)
                    .As<IUserRepository>();
                builder.RegisterInstance(fakeUserRoleRepository)
                    .As<IUserRoleRepository>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Add_WhenAddNewTicketManagementUser_ShouldSaveNewDataInRepository()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            var dto = this.Fixture.Build<TicketManagementUser>().Create();
            var expected = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 1, UserName = "admin", Email = "admin@admin.com", PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Admin", Surname = "Admin", Balance = 100000,  SecurityStamp = "SomeSecureStamp", },
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 300 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
                dto,
            };

            // Act
            var entityId = this.userService.Add(dto);

            // Assert
            using (new AssertionScope())
            {
                this.userRepository.GetAll().Should().BeEquivalentTo(expected);
                dto.Id.Should().Be(entityId);
            }
        }

        [Test]
        public void AddRole_WhenAddNewRoleToTicketManagementUser_ShouldSaveNewDataInRepository()
        {
            // Arrange
            this.userRoleRepository = this.Mock.Create<IUserRoleRepository>();
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            var role = new Role() { Id = 2, Name = "user" };
            var expected = new List<string>
            {
                "admin",
                "user",
            };

            // Act
            this.userService.AddRole(userId, role.Name);

            // Assert
            this.userRoleRepository.GetRoleNamesByUserId(userId).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Delete_WhenDeleteTicketManagementUserWithExitingUserId_ShouldDeleteDataFromRepository()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            var dto = this.Fixture.Build<TicketManagementUser>()
                .With(e => e.Id, userId)
                .Create();

            var expected = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 300 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
            };

            // Act
            this.userService.Delete(dto);

            // Assert
            this.userRepository.GetAll().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteRole_WhenDeleteUserRoleWithExistingUserId_ShouldDeleteRoleFromUser()
        {
            // Arrange
            this.userRoleRepository = this.Mock.Create<IUserRoleRepository>();
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            const string roleName = "admin";
            IEnumerable<string> expected = new List<string>();

            // Act
            this.userService.DeleteRole(userId, roleName);

            // Assert
            this.userRoleRepository.GetRoleNamesByUserId(userId).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FindById_WhenFindTicketManagementUserByExitingUserId_ShouldBeReturnThisUser()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;

            var expected = new TicketManagementUser()
            {
                Id = userId,
                UserName = "admin",
                Email = "admin@admin.com",
                PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==",
                TimeZone = "Belarus Standard Time",
                Language = "ru",
                FirstName = "Admin",
                Surname = "Admin",
                SecurityStamp = "SomeSecureStamp",
                Balance = 100000,
            };

            // Act
            var actual = this.userService.FindById(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FindByName_WhenFindTicketManagementUserByExitingUserName_ShouldBeReturnThisUser()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const string userName = "admin";

            var expected = new TicketManagementUser()
            {
                Id = 1,
                UserName = userName,
                Email = "admin@admin.com",
                PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==",
                TimeZone = "Belarus Standard Time",
                Language = "ru",
                FirstName = "Admin",
                Surname = "Admin",
                Balance = 100000,
                SecurityStamp = "SomeSecureStamp",
            };

            // Act
            var actual = this.userService.FindByName(userName);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetPasswordHash_WhenGetPasswordHashByExitingUserId_ShouldBeReturnPasswordHash()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            const string expected = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==";

            // Act
            var actual = this.userService.GetPasswordHash(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetRoles_WhenGetRolesByExistingUserId_ShouldBeReturnUserRoles()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            IEnumerable<string> expected = new List<string>()
            {
                "admin",
            };

            // Act
            var actual = this.userService.GetRoles(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetSecurityStamp_WhenGetSecurityStampByExistingUserId_ShouldBeReturnSecurityStamp()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            const string expected = "SomeSecureStamp";

            // Act
            var actual = this.userService.GetSecurityStamp(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void HasPassword_WhenHasPasswordByExistingUserIdWithPassword_ShouldBeReturnTrue()
        {
            // Arrange
            this.userService = this.Mock.Create<UserService>();
            const int userId = 1;
            const bool expected = true;

            // Act
            var actual = this.userService.HasPassword(userId);

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void SetPassword_WhenSetPasswordByExistingUserId_ShouldBeSavePassword()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            const int userId = 4;
            const string password = "testUser123";

            // Act
            this.userService.SetPassword(userId, password);

            // Assert
            this.userRepository.GetById(userId).PasswordHash.Should().Be(password);
        }

        [Test]
        public void SetSecurityStamp_WhenSetSecurityStampByExistingUserId_ShouldBeSaveSecurityStamp()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            const int userId = 4;
            const string securityStamp = "SomeSecurityStamp";

            // Act
            this.userService.SetSecurityStamp(userId, securityStamp);

            // Assert
            this.userRepository.GetById(userId).SecurityStamp.Should().Be(securityStamp);
        }

        [Test]
        public void Update_WhenUpdateTicketManagementUser_ShouldBeSaveChanges()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            var user = new TicketManagementUser()
            {
                Id = 4,
                UserName = "test user changes",
                Email = "testChanges@test.com",
                TimeZone = "England Standard Time",
                Language = "en",
                FirstName = "TestUserChange",
                Surname = "TestUserChange",
                Balance = 1500,
            };

            // Act
            this.userService.Update(user);

            // Assert
            this.userRepository.GetById(user.Id).Should().BeEquivalentTo(user);
        }

        [Test]
        public void IncreaseBalance_WhenIncreaseBalanceByExistingUserName_ShouldBeIncreaseBalance()
        {
            // Arrange
            this.userRepository = this.Mock.Create<IUserRepository>();
            this.userService = this.Mock.Create<UserService>();
            const string userName = "test user";
            const decimal increasingMoney = 50;
            const decimal expectedBalance = 100;

            // Act
            this.userService.IncreaseBalance(increasingMoney, userName);

            // Assert
            this.userRepository.FindByNormalizedUserName(userName).Balance.Should().Be(expectedBalance);
        }
    }
}
