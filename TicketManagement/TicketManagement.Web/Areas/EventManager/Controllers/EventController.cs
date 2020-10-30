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
        [ValidateAntiForgeryToken]
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
                return this.View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventViewModel @event)
        {
            if (!this.ModelState.IsValid)
            {
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                return this.View(@event);
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
                return this.View(@event);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!this.ModelState.IsValid) return this.RedirectToAction("Index");

            try
            {
                this.eventServiceEventManager.DeleteEvent(id);
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Delete", ex.Message);
                return this.View("Index");
            }
        }

        [HttpPost]
        public ActionResult Publish(int id)
        {
            try
            {
                this.eventServiceEventManager.PublishEvent(id);
                return this.View("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Publish", ex.Message);
                return this.View("Index");
            }
        }

        [HttpGet]
        public ActionResult EventDetail(int eventId)
        {
            return this.View(this.eventServiceEventManager.GetEventDetailViewModel(eventId));
        }

        [HttpGet]
        public ActionResult EventAreaDetail(int eventAreaId)
        {
            return this.View(this.eventServiceEventManager.GetEventAreaDetailViewModel(eventAreaId));
        }

        [HttpGet]
        public ActionResult ChangeCost(int areaId)
        {
            return this.View(this.eventServiceEventManager.GetAreaViewModel(areaId));
        }

        [HttpPost]
        public void ChangeCost(AreaViewModel areaVm)
        {
            this.eventServiceEventManager.ChangeCost(areaVm);
        }
    }
}