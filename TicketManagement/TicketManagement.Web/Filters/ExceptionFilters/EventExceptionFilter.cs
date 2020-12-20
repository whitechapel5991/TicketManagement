// ****************************************************************************
// <copyright file="EventExceptionFilter.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Net;
using System.Web.Mvc;
using TicketManagement.Web.Filters.Base;
using TicketManagement.Web.OrderService;

namespace TicketManagement.Web.Filters.ExceptionFilters
{
    public class EventExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception.GetType() == typeof(SeatCurrentlySoldOrBlockedException))
            {
                var errorMessage = Resources.TicketManagementResource.SeatIsNotFree;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
            }
        }
    }
}