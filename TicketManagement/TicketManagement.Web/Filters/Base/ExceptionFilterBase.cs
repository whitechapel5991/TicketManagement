// ****************************************************************************
// <copyright file="ExceptionFilterBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters.Base
{
    public class ExceptionFilterBase : HandleErrorAttribute
    {
        private static readonly ReaderWriterLock Locker = new ReaderWriterLock();

        protected void UpdateFilterContext(ExceptionContext filterContext, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected void LogUnknownException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var message = "Date :" + DateTime.Now + ", Controller: " + controllerName + ", Action:" + actionName +
                          "Error Message : " + exceptionMessage
                          + Environment.NewLine + "Stack Trace : " + stackTrace
                          + Environment.NewLine + "=================================================" + Environment.NewLine;

            try
            {
                Locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/LogExceptions.txt"), message);
            }
            finally
            {
                Locker.ReleaseWriterLock();
            }
        }
    }
}