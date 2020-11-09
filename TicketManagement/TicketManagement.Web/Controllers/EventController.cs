// ****************************************************************************
// <copyright file="EventController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketManagement.Web.Filters.AcionFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Controllers
{
    [EventExceptionFilter]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(
            IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        [AjaxContentUrl]
        public PartialViewResult Events()
        {
            return this.PartialView(this.eventService.GetPublishEvents());
        }

        [HttpGet]
        [Authorize]
        [AjaxContentUrl]
        public PartialViewResult EventDetail(int eventId)
        {
            var eventAreaDetailVm = this.eventService.GetEventDetailViewModel(eventId);
            return this.PartialView(eventAreaDetailVm);
        }

        [HttpGet]
        [Authorize]
        [AjaxContentUrl]
        public PartialViewResult EventAreaDetail(int eventAreaId)
        {
            var eventAreaDto = this.eventService.GetEventAreaDetailViewModel(eventAreaId);
            return this.PartialView(eventAreaDto);
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddToCart(int seatId)
        {
            var eventAreaId = this.eventService.AddToCart(seatId, this.User.Identity.GetUserId<int>());
            return this.Json(new { returnContentUrl = this.Url.Action("EventAreaDetail", new { controller = "Event", eventAreaId }) });
        }
    }
}