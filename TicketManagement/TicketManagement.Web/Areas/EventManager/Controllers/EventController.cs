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
    [LogCustomExceptionFilter]
    [Authorize(Roles = "event manager, admin")]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILayoutService layoutService;
        private readonly IEventAreaService eventAreaService;
        private readonly IEventManagerEventService eventServiceEventManager;

        public EventController(
            IEventService eventService,
            ILayoutService layoutService,
            IEventAreaService eventAreaService,
            IEventManagerEventService eventServiceEventManager
            )
        {
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.eventServiceEventManager = eventServiceEventManager;
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.eventServiceEventManager.GetIndexEventViewModels());
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        [HttpGet]
        public ActionResult Create()
        {
            return this.View(this.eventServiceEventManager.GetEventViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "event manager, admin")]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Create(EventViewModel eventViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                eventViewModel.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                return this.View(eventViewModel);
            }

            try
            {
                this.eventServiceEventManager.CreateEvent(eventViewModel);

                return this.RedirectToAction("GetAllEvents");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("EventCreate", ex.Message);
                eventViewModel.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                return this.View(eventViewModel);
            }
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                return this.View(this.eventServiceEventManager.GetEventViewModel(id));
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Update", ex.Message);
                return this.View("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "event manager, admin")]
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

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Update", ex.Message);
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                return this.View(@event);
            }
        }

        [Authorize(Roles = "event manager, admin")]
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

        [Authorize(Roles = "event manager, admin")]
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

        [Authorize(Roles = "admin, event manager")]
        [HttpGet]
        public ActionResult EventDetail(int id)
        {
            var eventDto = this.eventService.GetEventMap(id);

            return this.View(eventDto);
        }

        [Authorize(Roles = "admin, event manager")]
        [HttpGet]
        public ActionResult EventAreaDetail(int id)
        {
            var eventAreaDto = this.eventAreaService.GetEventAreaMap(id);

            return this.View(eventAreaDto);
        }

        [Authorize(Roles = "admin, event manager")]
        [HttpGet]
        public ActionResult ChangeCost(int areaId)
        {
            return this.View(this.eventServiceEventManager.GetAreaViewModel(areaId));
        }

        [Authorize(Roles = "admin, event manager")]
        [HttpPost]
        public void ChangeCost(AreaViewModel areaVm)
        {
            this.eventServiceEventManager.ChangeCost(areaVm);
        }
    }
}