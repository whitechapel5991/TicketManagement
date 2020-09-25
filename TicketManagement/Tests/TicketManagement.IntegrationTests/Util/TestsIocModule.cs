// ****************************************************************************
// <copyright file="TestsIocModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Configuration;
using Autofac;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;

namespace TicketManagement.IntegrationTests.Util
{
    internal class TestsIocModule : Module
    {
        private readonly string connectionString;

        public TestsIocModule()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterModule(new AdoAutofacModule(this.connectionString));
            builder.RegisterModule(new ServiceAutofacModule());
        }
    }
}
