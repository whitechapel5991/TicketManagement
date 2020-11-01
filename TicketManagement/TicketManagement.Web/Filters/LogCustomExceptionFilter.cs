using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TicketManagement.Web.Filters
{
    public class LogCustomExceptionFilter : HandleErrorAttribute
    {
        static ReaderWriterLock locker = new ReaderWriterLock();

        public override void OnException(ExceptionContext filterContext)
        {
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
                locker.AcquireWriterLock(int.MaxValue); 
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/LogExceptions.txt"), message);
            }
            finally
            {
                locker.ReleaseWriterLock();
            }

            filterContext.ExceptionHandled = false;
        }
    }
}