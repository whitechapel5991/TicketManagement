using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using Owin;
using TicketManagement.Web.Util;

namespace TicketManagement.Web
{
    public static class IocContainerConfig
    {
        public static IContainer Container { get; private set; }

        public static void ConfigureContainer(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WebIocModule(
                ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString,
                ConfigurationManager.ConnectionStrings["TicketManagementTest"].ConnectionString));

            Container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(Container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}