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

namespace TicketManagement.UnitTests.Util
{
    internal class TestsIocModule : Module
    {
        // private readonly ConfigurationBuilder config;
        private readonly string connectionString;
        private readonly string providerName;

        public TestsIocModule()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["TicketManagement"].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings["TicketManagement"].ProviderName;

            // config = new ConfigurationBuilder();
            ////config.SetBasePath(Directory.GetCurrentDirectory());
            // config.AddJsonFile("autofac.json");
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            // var module = new ConfigurationModule(config.Build());
            // var builder = new ContainerBuilder();
            builder.RegisterModule(new DalAutofacModule(this.connectionString, this.providerName));
            builder.RegisterModule(new BllAutofacModule());
        }
    }
}
