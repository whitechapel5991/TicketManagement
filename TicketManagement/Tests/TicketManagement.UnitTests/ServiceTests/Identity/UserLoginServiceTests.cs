// ****************************************************************************
// <copyright file="UserLoginServiceTests.cs" company="EPAM Systems">
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
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.BLL.Services.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;
using TicketManagement.UnitTests.FakeRepositories;

namespace TicketManagement.UnitTests.ServiceTests.Identity
{
    [TestFixture]
    internal class UserLoginServiceTests
    {
        private IUserLoginsService userLoginsService;

        private IUserLoginRepository userLoginRepository;

        private AutoMock Mock { get; set; }

        [SetUp]
        public void Init()
        {
            var userLogins = new List<UserLogin>
            {
                new UserLogin() { ProviderKey = "test provider key 1", LoginProvider = "test login provider 1", UserId = 1 },
                new UserLogin() { ProviderKey = "test provider key 2", LoginProvider = "test login provider 2", UserId = 1 },
                new UserLogin() { ProviderKey = "test provider key 3", LoginProvider = "test login provider 3", UserId = 3 },
                new UserLogin() { ProviderKey = "test provider key 4", LoginProvider = "test login provider 4", UserId = 4 },
            };

            var fakeUserLoginRepository = new UserLoginRepositoryFake(userLogins);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeUserLoginRepository)
                    .As<IUserLoginRepository>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Add_WhenAddNewUserLogin_ShouldSaveNewDataInRepository()
        {
            // Arrange
            this.userLoginRepository = this.Mock.Create<IUserLoginRepository>();
            this.userLoginsService = this.Mock.Create<UserLoginService>();
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
            this.userLoginRepository = this.Mock.Create<IUserLoginRepository>();
            this.userLoginsService = this.Mock.Create<UserLoginService>();
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
            this.userLoginsService = this.Mock.Create<UserLoginService>();
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
            this.userLoginRepository = this.Mock.Create<IUserLoginRepository>();
            this.userLoginsService = this.Mock.Create<UserLoginService>();
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
