using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters.Base
{
    public class ExceptionFilterBase : HandleErrorAttribute
    {
        protected static void UpdateFilterContext(ExceptionContext filterContext, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}