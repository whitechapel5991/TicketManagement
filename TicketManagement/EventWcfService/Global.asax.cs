using System;
using System.Configuration;
using Autofac;
using Autofac.Integration.Wcf;
using EventWcfService.Util;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;

namespace EventWcfService
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

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}