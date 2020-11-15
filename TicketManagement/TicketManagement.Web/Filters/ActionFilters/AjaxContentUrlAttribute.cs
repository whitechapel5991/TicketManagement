// ****************************************************************************
// <copyright file="AjaxContentUrlAttribute.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;

namespace TicketManagement.Web.Filters.ActionFilters
{
    public class AjaxContentUrlAttribute : FilterAttribute, IActionFilter
    {

        public static string ActionContentUrl { get; private set; } = "Index";

        public static string ControllerContentUrl { get; private set; } = "StartApp";

        public static string AreaContentUrl { get; private set; } = "";

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var area = filterContext.RouteData.DataTokens["area"] ?? string.Empty;
            AreaContentUrl = area.ToString();
            ActionContentUrl = filterContext.ActionDescriptor.ActionName;
            ControllerContentUrl = filterContext.Controller.GetType().Name.Replace("Controller", string.Empty);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ignore
        }
    }
}