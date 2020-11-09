// ****************************************************************************
// <copyright file="CartExceptionFilter.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Net;
using System.Web.Mvc;
using TicketManagement.BLL.Exceptions.OrderExceptions;
using TicketManagement.Web.Filters.Base;

namespace TicketManagement.Web.Filters.ExceptionFilters
{
    public class CartExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception.GetType() == typeof(NotEnoughMoneyException))
            {
                var errorMessage = Resources.TicketManagementResource.NotEnoughMoneyException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
            }
        }
    }
}