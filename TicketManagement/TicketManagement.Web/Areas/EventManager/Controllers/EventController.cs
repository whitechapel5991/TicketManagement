// ****************************************************************************
// <copyright file="EventController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Filters.ActionFilters;
using TicketManagement.Web.Filters.AuthorizationFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Areas.EventManager.Controllers
{
    [Authorize(Roles = "event manager, admin")]
    [EventEventManagerExceptionFilter]
    public class EventController : Controller
    {
        private readonly IEventManagerEventService eventServiceEventManager;

        public EventController(
            IEventManagerEventService eventServiceEventManager)
        {
            this.eventServiceEventManager = eventServiceEventManager;
        }

        [HttpGet]
        [AjaxContentUrl]
        public PartialViewResult Index()
        {
            return this.PartialView(this.eventServiceEventManager.GetIndexEventViewModels());
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return this.PartialView(this.eventServiceEventManager.GetEventViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Create(EventViewModel eventViewModel)
        {
            this.eventServiceEventManager.CreateEvent(eventViewModel);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area = "EventManager", controller = "Event" }) });
        }

        [HttpGet]
        public PartialViewResult Update(int id)
        {
            return this.PartialView(this.eventServiceEventManager.GetEventViewModel(id));
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Update(EventViewModel @event)
        {
            this.eventServiceEventManager.UpdateEvent(@event);
            return this.Json(new { returnContentUrl = this.Url.Action("EventDetail", new { area = "EventManager", controller = "Event", eventId = @event.IndexEventViewModel.Id }) });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            this.eventServiceEventManager.DeleteEvent(id);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area = "EventManager", controller = "Event" }) });
        }

        [HttpPost]
        public JsonResult Publish(int id)
        {
            this.eventServiceEventManager.PublishEvent(id);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area = "EventManager", controller = "Event" }) });
        }

        [HttpGet]
        public PartialViewResult EventDetail(int eventId)
        {
            return this.PartialView(this.eventServiceEventManager.GetEventDetailViewModel(eventId));
        }

        [HttpGet]
        public PartialViewResult EventAreaDetail(int eventAreaId)
        {
            return this.PartialView(this.eventServiceEventManager.GetEventAreaDetailViewModel(eventAreaId));
        }

        [HttpGet]
        public PartialViewResult ChangeCost(int eventAreaId)
        {
            return this.PartialView(this.eventServiceEventManager.GetAreaPriceViewModel(eventAreaId));
        }

        [HttpPost]
        public JsonResult ChangeCost(AreaPriceViewModel areaPriceVm)
        {
            this.eventServiceEventManager.ChangeCost(areaPriceVm);
            return this.Json(new { success = true, newPrice = areaPriceVm.Price }, JsonRequestBehavior.AllowGet);
        }
    }
}