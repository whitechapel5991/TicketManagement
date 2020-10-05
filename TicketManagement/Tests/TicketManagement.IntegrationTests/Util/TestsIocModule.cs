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
        private readonly string email;
        private readonly string emailPassword;
        private readonly int lockTime;

        public TestsIocModule()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString;
            this.email = ConfigurationManager.AppSettings["Email"];
            this.emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            this.lockTime = int.Parse(ConfigurationManager.AppSettings["lockTime"]);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterModule(new EfAutofacModule(this.connectionString));
            builder.RegisterModule(new ServiceAutofacModule(this.email, this.emailPassword, this.lockTime));
        }
    }
}
