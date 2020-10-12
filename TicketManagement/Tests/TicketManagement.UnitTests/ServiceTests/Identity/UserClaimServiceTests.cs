// ****************************************************************************
// <copyright file="UserClaimServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Security.Claims;
using Autofac;
using Autofac.Extras.Moq;
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
    internal class UserClaimServiceTests
    {
        private IUserClaimService userClaimService;

        private IUserClaimRepository userClaimRepository;

        private AutoMock Mock { get; set; }

        [SetUp]
        public void Init()
        {
            var users = new List<TicketManagementUser>
            {
                new TicketManagementUser() { Id = 1, UserName = "admin", Email = "admin@admin.com", PasswordHash = "AN+dimATtnynaDlFB1TFqB0XWDWbytYMwSQMwWGVT2Pdd3ASxUmvmQSHY9eNc9DU9A==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Admin", Surname = "Admin", Balance = 100000, SecurityStamp = "SomeSecureStamp" },
                new TicketManagementUser() { Id = 2, UserName = "user", Email = "user@user.com", PasswordHash = "AM1rNg7ocbR18loZOEsl4dqaX+fKjjmt5UbeKNM32rNYLeDVfi0mtAXkE7etqtZjng==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "User", Surname = "User", Balance = 300 },
                new TicketManagementUser() { Id = 3, UserName = "event manager", Email = "manager@manager.com", PasswordHash = "AIIyby5JGrXPBYaW+uc3WDX68f5ol82JHm4FIi9UHTJSRmD5WzKrP7DfG0nRbbCMpw==", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "Manager", Surname = "Manager", Balance = 1000 },
                new TicketManagementUser() { Id = 4, UserName = "test user", Email = "test@test.com", TimeZone = "Belarus Standard Time", Language = "ru", FirstName = "TestUser", Surname = "TestUser", Balance = 50 },
            };
            var userClaims = new List<UserClaim>
            {
                new UserClaim() { Id = 1, ClaimType = "test claim type 1", ClaimValue = "test claim value 1", UserId = 1 },
                new UserClaim() { Id = 2, ClaimType = "test claim type 2", ClaimValue = "test claim value 2", UserId = 1 },
                new UserClaim() { Id = 3, ClaimType = "test claim type 3", ClaimValue = "test claim value 3", UserId = 3 },
                new UserClaim() { Id = 4, ClaimType = "test claim type 4", ClaimValue = "test claim value 4", UserId = 4 },
            };

            var fakeUserClaimRepository = new UserClaimRepositoryFake(userClaims, users);

            this.Mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(fakeUserClaimRepository)
                    .As<IUserClaimRepository>();
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.Mock?.Dispose();
        }

        [Test]
        public void Add_WhenAddNewClaimToExistingUser_ShouldSaveNewDataInRepository()
        {
            // Arrange
            this.userClaimRepository = this.Mock.Create<IUserClaimRepository>();
            this.userClaimService = this.Mock.Create<UserClaimService>();
            const int userId = 1;
            var claim = new Claim("some type", "some value");
            var expected = new List<UserClaim>
            {
                new UserClaim() { Id = 1, ClaimType = "test claim type 1", ClaimValue = "test claim value 1", UserId = 1 },
                new UserClaim() { Id = 2, ClaimType = "test claim type 2", ClaimValue = "test claim value 2", UserId = 1 },
                new UserClaim() { ClaimType = claim.Type, ClaimValue = claim.Value, UserId = userId },
            };

            // Act
            this.userClaimService.Add(userId, claim);

            // Assert
            this.userClaimRepository.GetByUserId(userId).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetClaims_WhenGetClaimsByExistingUserId_ShouldReturnAllUserClaims()
        {
            // Arrange
            this.userClaimService = this.Mock.Create<UserClaimService>();
            const int userId = 1;
            var expected = new List<Claim>
            {
                new Claim("test claim type 1", "test claim value 1"),
                new Claim("test claim type 2", "test claim value 2"),
            };

            // Act
            var actual = this.userClaimService.GetClaims(userId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Remove_WhenRemoveClaimByExistingUserId_ShouldBeDeleteFromRepository()
        {
            // Arrange
            this.userClaimRepository = this.Mock.Create<IUserClaimRepository>();
            this.userClaimService = this.Mock.Create<UserClaimService>();
            const int userId = 1;
            var deletingClaim = new Claim("test claim type 1", "test claim value 1");
            var expected = new List<UserClaim>
            {
                new UserClaim() { Id = 2, ClaimType = "test claim type 2", ClaimValue = "test claim value 2", UserId = 1 },
            };

            // Act
            this.userClaimService.Remove(userId, deletingClaim);

            // Assert
            this.userClaimRepository.GetByUserId(userId).Should().BeEquivalentTo(expected);
        }
    }
}
