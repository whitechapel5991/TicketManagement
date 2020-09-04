// ****************************************************************************
// <copyright file="TestBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Moq;
using TicketManagement.UnitTests.Util;

namespace TicketManagement.UnitTests.ServiceTests.Base
{
    public class TestBase : IDisposable
    {
        protected readonly AutoMock Mock;
        protected ContainerBuilder container;

        public TestBase()
        {
            this.container = ConfigIocContainer.GetContainerBuilder();
            this.Mock = AutoMock.GetLoose();
        }

        public void Dispose()
        {
            this.Mock.Dispose();
        }
    }
}
