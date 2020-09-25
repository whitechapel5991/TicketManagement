// ****************************************************************************
// <copyright file="TestBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using Autofac;
using AutoFixture;
using NUnit.Framework;
using TicketManagement.IntegrationTests.Helper;
using TicketManagement.IntegrationTests.Util;

namespace TicketManagement.IntegrationTests.TestBase
{
    public abstract class TestBase : IDisposable
    {
        protected IContainer Container { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void InitBase()
        {
            this.Fixture = new Fixture();

            if (DatabaseHelper.SnapshotExists())
            {
                DatabaseHelper.RestoreFromSnapshot();
                DatabaseHelper.DropSnapshot();
            }

            DatabaseHelper.CreateSnapshot();

            this.Container = ConfigIocContainer.GetIocContainer();
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseHelper.RestoreFromSnapshot();
            DatabaseHelper.DropSnapshot();
        }

        public virtual void Dispose()
        {
            this.Container?.Dispose();
        }
    }
}
