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
using Hangfire;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;
using TicketManagement.WcfService.Util;

namespace TicketManagement.WcfService
{
    public class Global : System.Web.HttpApplication
    {
        private BackgroundJobServer backgroundJobServer;

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterModule(new WcfModule());
            builder.RegisterModule(new EfModule(ConfigurationManager.ConnectionStrings["TicketManagement"].ConnectionString));
            builder.RegisterModule(new ServiceModule(
                ConfigurationManager.AppSettings["Email"],
                ConfigurationManager.AppSettings["EmailPassword"],
                int.Parse(ConfigurationManager.AppSettings["lockTime"]),
                ConfigurationManager.ConnectionStrings["TicketManagement"].ConnectionString
            ));

            // Set the dependency resolver. This works for both regular
            // WCF services and REST-enabled services.
            var container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            AutofacHostFactory.Container = container;

            this.backgroundJobServer = new BackgroundJobServer();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.backgroundJobServer.Dispose();
        }
    }
}