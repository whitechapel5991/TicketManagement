using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using TicketManagement.Web.Util;

namespace TicketManagement.Web
{
    public static class IocContainerConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WebIocModule(ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString, ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString));

            var container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}