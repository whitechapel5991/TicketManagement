// ****************************************************************************
// <copyright file="MvcApplication.asax.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TicketManagement.Web.Constants;
using TicketManagement.Web.EventService;

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
            const string cookieLangName = "lang";

            var cultureCookie = HttpContext.Current.Request.Cookies[cookieLangName];
            var cultureName = cultureCookie != null ? cultureCookie.Value : Language.En.ToString();

            if (!Enum.TryParse(cultureName, out Language _))
            {
                cultureName = Language.En.ToString();
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "$";
        }
    }
}