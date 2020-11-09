// ****************************************************************************
// <copyright file="LogAttribute.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagement.Web.Filters.ActionFilters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private static readonly ReaderWriterLock Locker = new ReaderWriterLock();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            this.Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            this.Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = $"{methodName}- controller:{controllerName} action:{actionName}" + Environment.NewLine;

            try
            {
                Locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/LogActions.txt"), message);
            }
            finally
            {
                Locker.ReleaseWriterLock();
            }
        }
    }
}