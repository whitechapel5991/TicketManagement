using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Constants.Extension;
using TicketManagement.Web.Exceptions.Account;

namespace TicketManagement.Web.Filters
{
    public class RedirectExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var resman = new ResourceManager("Ticketmanagement.Web.Resources.TicketManagementResource", typeof(Resources.TicketManagementResource).Assembly);
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            if (filterContext.Exception.GetType() == typeof(UserNameOrPasswordWrongException))
            {
                var errorMessage = resman.GetString("WrongCredentials");

                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonResult()
                {
                    Data = new { ErrorMessage = errorMessage },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    //MaxJsonLength = int.MaxValue,
                    //ContentType = MediaTypeNames.Text.Plain,
                    //ContentEncoding = System.Text.Encoding.UTF8,
                    
                };

                UpdateFilterContext(filterContext);
                base.OnException(filterContext);
                return;
            }

           

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

            filterContext.ExceptionHandled = true;

            UpdateFilterContext(filterContext, (int)HttpStatusCode.OK);
        }

        private static void UpdateFilterContext(ExceptionContext filterContext, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}