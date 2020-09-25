using Autofac;
using Autofac.Integration.Mvc;
using System.Configuration;
using System.Web.Mvc;
using TicketManagement.Web.Util;

namespace TicketManagement.Web.App_Start
{
    public class IocContainerConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WebIocModule(ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString));

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}