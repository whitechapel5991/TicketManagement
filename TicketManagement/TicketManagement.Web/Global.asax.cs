using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TicketManagement.Web.App_Start;
using TicketManagement.Web.Constants;

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
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            const string CookieLangName = "lang";

            var cultureCookie = HttpContext.Current.Request.Cookies[CookieLangName];
            var cultureName = cultureCookie != null ? cultureCookie.Value : Language.En.ToString();

            Language langEnum;
            if (!Enum.TryParse(cultureName, out langEnum))
            {
                cultureName = Language.En.ToString();
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "$";
        }
    }
}