using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Filters;

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
        private readonly IEventSeatService eventSeatService;

        public EventController(
            IEventService eventService,
            ILayoutService layoutService,
            IEventAreaService eventAreaService,
            IEventSeatService eventSeatService
            )
        {
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.eventSeatService = eventSeatService;
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        public ActionResult Index()
        {
            var eventDtos = this.eventService.GetEvents().ToList();

            var events = this.MapToIndexEventViewModel(eventDtos);

            return this.View(events);
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        public ActionResult Create()
        {
            List<Layout> layouts = this.layoutService.GetLayouts().ToList();
            Layout layout = layouts.First();

            EventViewModel vm = new EventViewModel
            {
                LayoutList = new SelectList(layouts, "Id", "Name", layout),
            };

            return this.View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "event manager, admin")]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Create(EventViewModel @event)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.eventService.AddEvent(
                        new Event 
                        { 
                            Name = @event.IndexEventViewModel.Name,
                            BeginDate = @event.IndexEventViewModel.BeginDate.AddHours(@event.IndexEventViewModel.BeginTime.Hour)
                                                .AddMinutes(@event.IndexEventViewModel.BeginTime.Minute)
                                                .AddSeconds(@event.IndexEventViewModel.BeginTime.Second),
                            EndDate = @event.IndexEventViewModel.EndDate.AddHours(@event.IndexEventViewModel.EndTime.Hour)
                                                .AddMinutes(@event.IndexEventViewModel.EndTime.Minute)
                                                .AddSeconds(@event.IndexEventViewModel.EndTime.Second),
                            Description = @event.IndexEventViewModel.Description,
                            LayoutId = @event.LayoutId,
                        });

                    return this.RedirectToAction("GetAllEvents");
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("EventCreate", ex.Message);
                    @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                    return this.View(@event);
                }
            }
            else
            {
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name");
                return this.View(@event);
            }
        }

        [Authorize(Roles = "event manager, admin")]
        [HandleError]
        public ActionResult Update(int? id)
        {
            try
            {
                var eventDto = this.eventService.GetEvent(id.Value);
                if (eventDto == null)
                {
                    return this.HttpNotFound();
                }

                var eventVM = MapToEventViewModel(eventDto);

                return this.View(eventVM);
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
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.eventService.UpdateEvent(new Event
                    {
                        Name = @event.IndexEventViewModel.Name,
                        BeginDate = @event.IndexEventViewModel.BeginDate.AddHours(@event.IndexEventViewModel.BeginTime.Hour)
                                                .AddMinutes(@event.IndexEventViewModel.BeginTime.Minute)
                                                .AddSeconds(@event.IndexEventViewModel.BeginTime.Second),
                        EndDate = @event.IndexEventViewModel.EndDate.AddHours(@event.IndexEventViewModel.EndTime.Hour)
                                                .AddMinutes(@event.IndexEventViewModel.EndTime.Minute)
                                                .AddSeconds(@event.IndexEventViewModel.EndTime.Second),
                        Description = @event.IndexEventViewModel.Description,
                        LayoutId = @event.LayoutId,
                        Published = @event.IndexEventViewModel.Published,
                        Id = @event.IndexEventViewModel.Id,
                    });

                    return this.RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Update", ex.Message);
                    @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                    return this.View(@event);
                }
            }
            else
            {
                @event.LayoutList = new SelectList(this.layoutService.GetLayouts(), "Id", "Name", @event.LayoutId);
                return this.View(@event);
            }
        }

        [Authorize(Roles = "event manager, admin")]
        public ActionResult Delete(int? id)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var eventDto = this.eventService.GetEvent(id.Value);
                    if (eventDto == null)
                    {
                        return this.HttpNotFound();
                    }

                    this.eventService.DeleteEvent(id.Value);
                    return this.RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Delete", ex.Message);
                    return this.View("Index");
                }
            }

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "event manager, admin")]
        public ActionResult Publish(int? id)
        {
            try
            {
                var eventDto = this.eventService.GetEvent(id.Value);
                if (eventDto == null)
                {
                    return this.HttpNotFound();
                }

                this.eventService.PublishEvent(id.Value);
                return this.View("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Publish", ex.Message);
                return this.View("Index");
            }
        }

        [Authorize(Roles = "admin, event manager")]
        public ActionResult EventDetail(int? id)
        {
            var eventDto = this.eventService.GetEventMap(id.Value);
            if (eventDto == null)
            {
                return this.HttpNotFound();
            }

            return this.View(eventDto);
        }

        [Authorize(Roles = "admin, event manager")]
        public ActionResult EventAreaDetail(int? id)
        {
            var eventAreaDto = this.eventAreaService.GetEventAreaMap(id.Value);
            if (eventAreaDto == null)
            {
                return this.HttpNotFound();
            }

            return this.View(eventAreaDto);
        }

        [Authorize(Roles = "admin, event manager")]
        [HttpGet]
        public ActionResult ChangeCost(int? seatId)
        {
            var seatCost = this.eventSeatService.GetSeatCost(seatId.Value);
            var seatVM = new SeatViewModel
            {
                EventSeatId = seatId.Value,
                Price = seatCost,
            };

            return this.View(seatVM);
        }

        [Authorize(Roles = "admin, event manager")]
        [HttpPost]
        public void ChangeCost(SeatViewModel seatVM)
        {
            var seat = this.eventSeatService.GetEventSeat(seatVM.EventSeatId);
            var area = this.eventAreaService.GetEventArea(seat.EventAreaId);
            area.Price = seatVM.Price;
            this.eventAreaService.UpdateEventArea(area);
        }

        private EventViewModel MapToEventViewModel(Event @event)
        {
            List<Layout> layouts = this.layoutService.GetLayouts().ToList();

            return new EventViewModel
            {
                IndexEventViewModel = new IndexEventViewModel
                {
                    Id = @event.Id,
                    BeginDate = @event.BeginDate,
                    BeginTime = @event.BeginDate,
                    Description = @event.Description,
                    EndDate = @event.EndDate,
                    EndTime = @event.EndDate,
                    LayoutId = @event.LayoutId,
                    Name = @event.Name,
                    Published = @event.Published,
                    LayoutName = this.layoutService.GetLayout(@event.LayoutId).Name,
                },
                LayoutId = @event.LayoutId,
                LayoutList = new SelectList(layouts, "Id", "Name", @event.LayoutId),
            };
        }

        private List<IndexEventViewModel> MapToIndexEventViewModel(List<Event> eventList)
        {
            List<IndexEventViewModel> eventVMList = new List<IndexEventViewModel>();

            foreach (var eventDto in eventList)
            {
                IndexEventViewModel vm = new IndexEventViewModel
                {
                    Id = eventDto.Id,
                    BeginDate = eventDto.BeginDate,
                    BeginTime = eventDto.BeginDate,
                    Description = eventDto.Description,
                    EndDate = eventDto.EndDate,
                    EndTime = eventDto.EndDate,
                    LayoutId = eventDto.LayoutId,
                    Name = eventDto.Name,
                    Published = eventDto.Published,
                    LayoutName = this.layoutService.GetLayout(eventDto.LayoutId).Name,
                };

                eventVMList.Add(vm);
            }

            return eventVMList;
        }
    }
}