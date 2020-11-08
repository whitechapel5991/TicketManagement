using System;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Filters.AcionFilters;
using TicketManagement.Web.Filters.AuthorizationFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;
using IEventService = TicketManagement.BLL.Interfaces.IEventService;

namespace TicketManagement.Web.Areas.EventManager.Controllers
{
    [Authorize(Roles = "event manager, admin")]
    public class EventController : Controller
    {
        private readonly ILayoutService layoutService;
        private readonly IEventManagerEventService eventServiceEventManager;

        public EventController(
            ILayoutService layoutService,
            IEventManagerEventService eventServiceEventManager
            )
        {
            this.layoutService = layoutService;
            this.eventServiceEventManager = eventServiceEventManager;
        }

        [HttpGet]
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
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area ="EventManager", controller = "Event" } ) });
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
            return this.Json(new { returnContentUrl = this.Url.Action("EventDetail", new { area ="EventManager", controller = "Event", eventId = @event.IndexEventViewModel.Id } ) });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            this.eventServiceEventManager.DeleteEvent(id);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area ="EventManager", controller = "Event" } ) });
        }

        [HttpPost]
        public JsonResult Publish(int id)
        {
            this.eventServiceEventManager.PublishEvent(id);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", new { area ="EventManager", controller = "Event" } ) });
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