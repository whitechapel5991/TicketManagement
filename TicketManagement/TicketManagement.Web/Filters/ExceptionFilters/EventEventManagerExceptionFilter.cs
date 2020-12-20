// ****************************************************************************
// <copyright file="EventEventManagerExceptionFilter.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Net;
using System.Web.Mvc;
using TicketManagement.Web.EventService;
using TicketManagement.Web.Filters.Base;

namespace TicketManagement.Web.Filters.ExceptionFilters
{
    public class EventEventManagerExceptionFilter : ExceptionFilterBase
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception.GetType() == typeof(EventInPastException))
            {
                var errorMessage = Resources.TicketManagementResource.EventInPastException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(BeginDateLongerThenEndDateException))
            {
                var errorMessage = Resources.TicketManagementResource.BeginDateLongerEndDateException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(EventExistInTheLayoutInThisTimeException))
            {
                var errorMessage = Resources.TicketManagementResource.CreateEventInLayoutInTheSameTimeException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(LayoutHasNotAreaException))
            {
                var errorMessage = Resources.TicketManagementResource.LayoutNotHasAreasException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(LayoutHasNotSeatException))
            {
                var errorMessage = Resources.TicketManagementResource.LayoutHasNotSeatException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(LayoutHasSoldSeatAndCouldNotBeChangeException))
            {
                var errorMessage = Resources.TicketManagementResource.EventIsNotDeletingException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(EventAlreadyPublishedException))
            {
                var errorMessage = Resources.TicketManagementResource.EventAlreadyPublishedException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
                return;
            }

            if (filterContext.Exception.GetType() == typeof(SomeAreaHasNotPriceException))
            {
                var errorMessage = Resources.TicketManagementResource.SetCostsEventException;

                filterContext.Result = new JsonResult
                {
                    Data = errorMessage,
                };
                this.UpdateFilterContext(filterContext, (int)HttpStatusCode.NotFound);
            }
        }
    }
}