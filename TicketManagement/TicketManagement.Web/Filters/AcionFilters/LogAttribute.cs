using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketManagement.Web.Filters.AcionFilters
{
    public class LogAttribute : ActionFilterAttribute
    {
        static ReaderWriterLock locker = new ReaderWriterLock();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = $"{methodName}- controller:{controllerName} action:{actionName}" + Environment.NewLine;

            try
            {
                locker.AcquireWriterLock(int.MaxValue); 
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/LogActions.txt"), message);
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
    }
}