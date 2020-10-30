using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters
{
    public class AjaxContentUrlAttribute : FilterAttribute, IActionFilter
    {
        public static string CurrentContentUrl { get; private set; } = "/TicketManagement.Web/Account/Login";

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string action = filterContext.ActionDescriptor.ActionName;
            string controller = filterContext.Controller.GetType().Name.Replace("Controller", string.Empty);
            CurrentContentUrl = $"/TicketManagement.Web/{controller}/{action}";
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ignore
        }
    }
}