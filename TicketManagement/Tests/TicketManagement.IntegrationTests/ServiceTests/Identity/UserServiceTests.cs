// ****************************************************************************
// <copyright file="UserServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using Autofac;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests.Identity
{
    [TestFixture]
    internal class UserServiceTests : Test
    {
        private IUserService userService;

        private IUserRepository userRepository;

        private IUserRoleRepository userRoleRepository;

        [SetUp]
        public void Init()
        {
            this.userService = this.Container.Resolve<IUserService>();
            this.userRepository = this.Container.Resolve<IUserRepository>();
            this.userRoleRepository = this.Container.Resolve<IUserRoleRepository>();
        }

        [Test]
        public void Add_WhenAddNewTicketManagementUser_ShouldSaveNewDataInDatabase()
        {
            // Arrange
            var userDto = new TicketManagementUser
            {
                Email = "sbaka1111@gmail.com",
                Password = "adminadmin",
                UserName = "Den",
                FirstName = "perec",
                Surname = "paprika",
                Language = "ru",
                TimeZone = "Belarus",
            };

            // Act
            this.userService.Add(userDto);

            // Assert
            this.userRepository.FindByNormalizedUserName("Den").Should().BeEquivalentTo(userDto);
        }

        [Test]
        public void AddRole_WhenAddNewRoleToTicketManagementUser_ShouldSaveNewDataInDatabase()
        {
            // Arrange
            const int userId = 1;
            const string roleName = "user";
            var expected = new List<string>
            {
                "admin",
                "user",
            };

            // Act
            this.userService.AddRole(userId, roleName);

            // Assert
            expected.Should().BeEquivalentTo(this.userRoleRepository.GetRoleNamesByUserId(userId));
        }

        [Test]
        public void Delete_WhenDeleteTicketManagementUserWithExitingUserId_ShouldDeleteDataFromDatabase()
        {
            // Arrange
            const int userId = 1;
            var dto = this.Fixture.Build<TicketManagementUser>()
                .With(e => e.Id, userId)
                .Create();

            var expected = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "en", FirstName = "User", Surname = "UserS", Balance = 300, EmailConfirmed = true },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "ManagerS", Balance = 1000, EmailConfirmed = true },
            };

            // Act
            this.userService.Delete(dto);

            // Assert
            expected.Should().BeEquivalentTo(this.userRepository.GetAll());
        }

        [Test]
        public void DeleteRole_WhenDeleteUserRoleWithExistingUserId_ShouldDeleteRoleFromUserRoleTable()
        {
            // Arrange
            const int userId = 1;
            const string roleName = "admin";
            IEnumerable<string> expected = new List<string>();

            // Act
            this.userService.DeleteRole(userId, roleName);

            // Assert
            expected.Should().BeEquivalentTo(this.userRoleRepository.GetRoleNamesByUserId(userId));
        }

        [Test]
        public void FindById_WhenFindTicketManagementUserByExitingUserId_ShouldBeReturnThisUser()
        {
            // Arrange
            const int userId = 1;

            var expected = new TicketManagementUser()
            {
                Id = userId,
                UserName = "admin",
                Email = "whitechapel5991@gmail.com",
                PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==",
                TimeZone = "Belarus Standard Time",
                Language = "ru",
                FirstName = "Admin",
                Surname = "AdminS",
                Balance = 100000,
                EmailConfirmed = true,
            };

            // Act
            var actual = this.userService.FindById(userId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void FindByName_WhenFindTicketManagementUserByExitingUserName_ShouldBeReturnThisUser()
        {
            // Arrange
            const string userName = "admin";

            var expected = new TicketManagementUser()
            {
                Id = 1,
                UserName = userName,
                Email = "whitechapel5991@gmail.com",
                PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==",
                TimeZone = "Belarus Standard Time",
                Language = "ru",
                FirstName = "Admin",
                Surname = "AdminS",
                Balance = 100000,
                EmailConfirmed = true,
            };

            // Act
            var actual = this.userService.FindByName(userName);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetPasswordHash_WhenGetPasswordHashByExitingUserId_ShouldBeReturnPasswordHash()
        {
            // Arrange
            const int userId = 1;
            const string expected = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==";

            // Act
            var actual = this.userService.GetPasswordHash(userId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetRoles_WhenGetRolesByExistingUserId_ShouldBeReturnUserRoles()
        {
            // Arrange
            const int userId = 1;
            IEnumerable<string> expected = new List<string>()
            {
                "admin",
            };

            // Act
            var actual = this.userService.GetRoles(userId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetSecurityStamp_WhenGetSecurityStampByExistingUserId_ShouldBeReturnSecurityStamp()
        {
            // Arrange
            const int userId = 2;
            const string expected = "SomeSecureStamp";

            // Act
            var actual = this.userService.GetSecurityStamp(userId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void HasPassword_WhenHasPasswordByExistingUserIdWithPassword_ShouldBeReturnTrue()
        {
            // Arrange
            const int userId = 1;
            const bool expected = true;

            // Act
            var actual = this.userService.HasPassword(userId);

            // Assert
            expected.Should().Be(actual);
        }

        [Test]
        public void SetPassword_WhenSetPasswordByExistingUserId_ShouldBeSavePassword()
        {
            // Arrange
            const int userId = 1;
            const string password = "testUser123";

            // Act
            this.userService.SetPassword(userId, password);

            // Assert
            password.Should().Be(this.userRepository.GetById(userId).PasswordHash);
        }

        [Test]
        public void SetSecurityStamp_WhenSetSecurityStampByExistingUserId_ShouldBeSaveSecurityStamp()
        {
            // Arrange
            const int userId = 1;
            const string securityStamp = "SomeSecurityStamp";

            // Act
            this.userService.SetSecurityStamp(userId, securityStamp);

            // Assert
            securityStamp.Should().Be(this.userRepository.GetById(userId).SecurityStamp);
        }

        [Test]
        public void Update_WhenUpdateTicketManagementUser_ShouldBeSaveChanges()
        {
            // Arrange
            var user = new TicketManagementUser()
            {
                Id = 1,
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
            user.Should().BeEquivalentTo(this.userRepository.GetById(user.Id));
        }

        [Test]
        public void IncreaseBalance_WhenIncreaseBalanceByExistingUserName_ShouldBeIncreaseBalance()
        {
            // Arrange
            const string userName = "User";
            const decimal increasingMoney = 50;
            const decimal expectedBalance = 350;

            // Act
            this.userService.IncreaseBalance(increasingMoney, userName);

            // Assert
            expectedBalance.Should().Be(this.userRepository.FindByNormalizedUserName(userName).Balance);
        }
    }
}
