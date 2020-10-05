// ****************************************************************************
// <copyright file="OrderServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Interfaces.Identity;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class OrderServiceTests : Test
    {
        private IOrderService orderService;

        [SetUp]
        public void Init()
        {
            this.orderService = this.Container.Resolve<IOrderService>();
        }

        [Test]
        public void Create_AddNewUser_GetNewUserByName()
        {
            int eventSeatId = 2;

            this.orderService.AddToCart(eventSeatId, 1);
        }
    }
}
