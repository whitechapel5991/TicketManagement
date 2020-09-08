// ****************************************************************************
// <copyright file="TestBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using Autofac;
using NUnit.Framework;
using TicketManagement.IntegrationTests.Util;

namespace TicketManagement.IntegrationTests.TestBase
{
    public abstract class TestBase : IDisposable
    {
        protected IContainer Container { get; private set; }

        [SetUp]
        public void InitBase()
        {
            this.Container = ConfigIocContainer.GetIocContainer();
        }

        public virtual void Dispose()
        {
            this.Container?.Dispose();
        }
    }
}
