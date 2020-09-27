using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.Interfaces;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    class OrderServiceTests : Test
    {
        private IOrderService orderService;
        private IUserService userService;

        [SetUp]
        public void Init()
        {
            this.orderService = this.Container.Resolve<IOrderService>();
            this.userService = this.Container.Resolve<IUserService>();
        }

        [Test]
        public void Create_AddNewUser_GetNewUserByName()
        {
            int orderId = 1;
            int eventSeatId = 2;

            this.orderService.AddToCart(eventSeatId, 1);

            //expected.Should().BeEquivalentTo(results);
        }
    }
}
