using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using TicketManagement.Web.Util;

namespace TicketManagement.Web
{
    public static class IocContainerConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WebIocModule(ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString));

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}