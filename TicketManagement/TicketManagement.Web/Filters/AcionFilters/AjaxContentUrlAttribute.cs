// ****************************************************************************
// <copyright file="AjaxContentUrlAttribute.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;

namespace TicketManagement.Web.Filters.AcionFilters
{
    public class AjaxContentUrlAttribute : FilterAttribute, IActionFilter
    {
        public static string CurrentContentUrl { get; private set; } = "/TicketManagement.Web/Account/Login";

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var action = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.Controller.GetType().Name.Replace("Controller", string.Empty);
            CurrentContentUrl = $"/TicketManagement.Web/{controller}/{action}";
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ignore
        }
    }
}