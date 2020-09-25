// ****************************************************************************
// <copyright file="UserServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using Autofac;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TicketManagement.BLL.Infrastructure;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models.Identity;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests : Test
    {
        private IUserService userService;

        [SetUp]
        public void Init()
        {
            this.userService = this.Container.Resolve<IUserService>();
        }

        [Test]
        public void Create_AddNewUser_GetNewUserByName()
        {
            TicketManagementUser userDto = new TicketManagementUser
            {
                Email = "sbaka1111@gmail.com",
                Password = "adminadmin",
                UserName = "Den",
                FirstName = "perec",
                Surname = "paprika",
                Language = "ru",
                TimeZone = "Belarus",
            };

            var expected = new OperationDetails(true, "Registration successfull", string.Empty);

            var results = this.userService.Create(userDto);

            expected.Should().BeEquivalentTo(results);
        }

        [Test]
        public void Authenticate_AuthenticateUser_GetIdentityClaims()
        {
            var dto = this.Fixture.Build<TicketManagementUser>()
                .With(x => x.UserName, "admin")
                .With(x => x.Password, "adminadmin")
                .Create();

            var results = this.userService.Authenticate(dto);
        }
    }
}
