using System;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;
using IEventService = TicketManagement.BLL.Interfaces.IEventService;

namespace TicketManagement.Web.Areas.EventManager.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [Authorize(Roles = "event manager, admin")]
    [RedirectExceptionFilter]
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
        public ActionResult Index()
        {
            return this.PartialView(this.eventServiceEventManager.GetIndexEventViewModels());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.PartialView(this.eventServiceEventManager.GetEventViewModel());
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                eventViewModel.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                return this.PartialView(eventViewModel);
            }

            try
            {
                this.eventServiceEventManager.CreateEvent(eventViewModel);

                return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("EventCreate", ex.Message);
                eventViewModel.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                return this.View(eventViewModel);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                return this.PartialView(this.eventServiceEventManager.GetEventViewModel(id));
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Update", ex.Message);
                return this.PartialView("Index");
            }
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Update(EventViewModel @event)
        {
            if (!this.ModelState.IsValid)
            {
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                return this.PartialView(@event);
            }

            try
            {
                this.eventServiceEventManager.UpdateEvent(@event);

                return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Update", ex.Message);
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                return this.PartialView(@event);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!this.ModelState.IsValid) return this.RedirectToAction("Index");

            try
            {
                this.eventServiceEventManager.DeleteEvent(id);
                return this.PartialView("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Delete", ex.Message);
                return this.PartialView("Index");
            }
        }

        [HttpPost]
        public ActionResult Publish(int id)
        {
            try
            {
                this.eventServiceEventManager.PublishEvent(id);
                return this.PartialView("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Publish", ex.Message);
                return this.PartialView("Index");
            }
        }

        [HttpGet]
        public ActionResult EventDetail(int eventId)
        {
            return this.PartialView(this.eventServiceEventManager.GetEventDetailViewModel(eventId));
        }

        [HttpGet]
        public ActionResult EventAreaDetail(int eventAreaId)
        {
            return this.PartialView(this.eventServiceEventManager.GetEventAreaDetailViewModel(eventAreaId));
        }

        [HttpGet]
        public ActionResult ChangeCost(int eventAreaId)
        {
            return this.PartialView(this.eventServiceEventManager.GetAreaPriceViewModel(eventAreaId));
        }

        [HttpPost]
        public ActionResult ChangeCost(AreaPriceViewModel areaPriceVm)
        {
            this.eventServiceEventManager.ChangeCost(areaPriceVm);
            return this.Json(new { success = true, newPrice = areaPriceVm.Price }, JsonRequestBehavior.AllowGet);
        }
    }
}