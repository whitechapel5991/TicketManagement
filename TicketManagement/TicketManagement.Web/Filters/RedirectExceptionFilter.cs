using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Constants.Extension;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters.Base;

namespace TicketManagement.Web.Filters
{
    public class RedirectExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            if (filterContext.Controller.ControllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var isEventManager = filterContext.Controller.ControllerContext.HttpContext.User.IsInRole(Roles.UserManager.GetStringValue());
                var isAdmin = filterContext.Controller.ControllerContext.HttpContext.User.IsInRole(Roles.Admin.GetStringValue());

                if (isEventManager || isAdmin)
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "AdminError",
                        MasterName = this.Master,
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                        TempData = filterContext.Controller.TempData,
                    };
                }
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "UserError",
                };
            }

            UpdateFilterContext(filterContext);
        }
    }
}