// ****************************************************************************
// <copyright file="UserLoginServiceTests.cs" company="EPAM Systems">
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
    internal class UserLoginServiceTests : Test
    {
        private IUserLoginsService userLoginsService;

        private IUserLoginRepository userLoginRepository;

        [SetUp]
        public void Init()
        {
            this.userLoginsService = this.Container.Resolve<IUserLoginsService>();
            this.userLoginRepository = this.Container.Resolve<IUserLoginRepository>();
        }

        [Test]
        public void Add_WhenAddNewUserLogin_ShouldSaveNewDataInRepository()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                UserId = 1,
                ProviderKey = "Add provider key",
                LoginProvider = "Add login provider",
            };

            var expected = new List<UserLogin>
            {
                new UserLogin() { ProviderKey = "test provider key 1", LoginProvider = "test login provider 1", UserId = 1 },
                new UserLogin() { ProviderKey = "test provider key 2", LoginProvider = "test login provider 2", UserId = 1 },
                userLogin,
            };

            // Act
            this.userLoginsService.Add(userLogin);

            // Assert
            expected.Should().BeEquivalentTo(this.userLoginRepository.FindByUserId(userLogin.UserId));
        }

        [Test]
        public void DeleteUserLogin_WhenDeleteUserLoginByExistingUserId_ShouldBeRemoveFromRepository()
        {
            // Arrange
            var userLogin = new UserLogin
            {
                UserId = 1,
                ProviderKey = "test provider key 1",
                LoginProvider = "test login provider 1",
            };

            var expected = new List<UserLogin>
            {
                new UserLogin() { ProviderKey = "test provider key 2", LoginProvider = "test login provider 2", UserId = 1 },
            };

            // Act
            this.userLoginsService.DeleteUserLogin(userLogin.UserId, userLogin);

            // Assert
            expected.Should().BeEquivalentTo(this.userLoginRepository.FindByUserId(userLogin.UserId));
        }

        [Test]
        public void Find_WhenFindUserLoginByExistingUserLoginKey_ShouldBeReturnUserLogin()
        {
            // Arrange
            var userLogin = new UserLoginKey
            {
                ProviderKey = "test provider key 1",
                LoginProvider = "test login provider 1",
            };

            var expected = new UserLogin()
                { ProviderKey = "test provider key 1", LoginProvider = "test login provider 1", UserId = 1 };

            // Act
            var actual = this.userLoginsService.Find(userLogin);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetLoginsByUserId_WhenGetUserLoginsByExistingUserId_ShouldBeReturnAllUserLogins()
        {
            // Arrange
            const int userId = 1;

            var expected = new List<UserLogin>
            {
                new UserLogin() { ProviderKey = "test provider key 1", LoginProvider = "test login provider 1", UserId = 1 },
                new UserLogin() { ProviderKey = "test provider key 2", LoginProvider = "test login provider 2", UserId = 1 },
            };

            // Act
            var actual = this.userLoginsService.GetLoginsByUserId(userId);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }
    }
}
