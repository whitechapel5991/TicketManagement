// ****************************************************************************
// <copyright file="UserClaimServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Security.Claims;
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
    internal class UserClaimServiceTests : Test
    {
        private IUserClaimService userClaimService;

        private IUserClaimRepository userClaimRepository;

        [SetUp]
        public void Init()
        {
            this.userClaimService = this.Container.Resolve<IUserClaimService>();
            this.userClaimRepository = this.Container.Resolve<IUserClaimRepository>();
        }

        [Test]
        public void Add_WhenAddNewClaimToExistingUser_ShouldSaveNewDataInRepository()
        {
            // Arrange
            const int userId = 1;
            var claim = new Claim("some type", "some value");
            var expected = new List<UserClaim>
            {
                new UserClaim() { Id = 1, ClaimType = "test claim type 1", ClaimValue = "test claim value 1", UserId = 1 },
                new UserClaim() { Id = 2, ClaimType = "test claim type 2", ClaimValue = "test claim value 2", UserId = 1 },
                new UserClaim() { Id = 5, ClaimType = claim.Type, ClaimValue = claim.Value, UserId = userId },
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
