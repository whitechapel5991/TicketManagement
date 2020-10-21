using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters
{
    public class LogCustomExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var message = "Date :" + DateTime.Now + ", Controller: " + controllerName + ", Action:" + actionName +
                          "Error Message : " + exceptionMessage
                          + Environment.NewLine + "Stack Trace : " + stackTrace;

            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/LogExceptions.txt"), message);
        }
    }
}