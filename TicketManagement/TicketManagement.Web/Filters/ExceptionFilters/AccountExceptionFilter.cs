using System.Net;
using System.Resources;
using System.Web.Mvc;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters.Base;

namespace TicketManagement.Web.Filters.ExceptionFilters
{
    public class AccountExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var resourceManager = new ResourceManager("Ticketmanagement.Web.Resources.TicketManagementResource", typeof(Resources.TicketManagementResource).Assembly);

            if (filterContext.Exception.GetType() == typeof(UserNameOrPasswordWrongException))
            {
                var errorMessage = resourceManager.GetString("WrongCredentials");

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                UpdateFilterContext(filterContext, (int) HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(RegisterUserWrongDataException))
            {
                var errorMessage = resourceManager.GetString("UserExistException");

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                UpdateFilterContext(filterContext, (int) HttpStatusCode.NotFound);
                return;
            }
        }
    }
}