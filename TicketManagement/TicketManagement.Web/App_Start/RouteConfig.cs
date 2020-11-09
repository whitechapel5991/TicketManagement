// ****************************************************************************
// <copyright file="RouteConfig.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagement.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "", controller = "StartApp", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TicketManagement.Web.Controllers" });
        }
    }
}
