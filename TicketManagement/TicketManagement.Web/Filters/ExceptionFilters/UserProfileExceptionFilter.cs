// ****************************************************************************
// <copyright file="UserProfileExceptionFilter.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Net;
using System.Web.Mvc;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Filters.Base;

namespace TicketManagement.Web.Filters.ExceptionFilters
{
    public class UserProfileExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception.GetType() == typeof(UserNameOrPasswordWrongException))
            {
                var errorMessage = Resources.TicketManagementResource.WrongCredentials;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(RegisterUserWrongDataException))
            {
                var errorMessage = Resources.TicketManagementResource.UserExistException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
            }
        }
    }
}