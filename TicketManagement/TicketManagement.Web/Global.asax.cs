using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TicketManagement.Web.App_Start;

namespace TicketManagement.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IocContainerConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
