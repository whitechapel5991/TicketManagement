// ****************************************************************************
// <copyright file="Global.asax.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Configuration;
using Autofac;
using Autofac.Integration.Wcf;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;
using TicketManagement.WcfService.Util;

namespace TicketManagement.WcfService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterModule(new WcfModule());
            builder.RegisterModule(new EfModule(ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString));
            builder.RegisterModule(new ServiceModule());

            // Set the dependency resolver. This works for both regular
            // WCF services and REST-enabled services.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}