﻿// ****************************************************************************
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
        private readonly string providerName;

        public TestsIocModule()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings["TicketManagementTest"].ProviderName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterModule(new AdoAutofacModule(this.connectionString, this.providerName));
            builder.RegisterModule(new ServiceAutofacModule());
        }
    }
}
