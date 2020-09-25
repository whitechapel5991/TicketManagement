using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Models.Event;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILayoutService layoutService;
        private readonly IEventAreaService eventAreaService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public EventController(
            IEventService eventService,
            ILayoutService layoutService,
            IEventAreaService eventAreaService,
            IOrderService orderService,
            IUserService userService
            )
        {
            this.orderService = orderService;
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult Events()
        {
            var eventDtos = this.eventService.GetPublishEvents();

            var events = this.MapToEventViewModel(eventDtos);

            return this.View(events);
        }

        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult EventDetail(int? id)
        {
            var eventDto = this.eventService.GetEventMap(id.Value);
            if (eventDto == null)
            {
                return this.HttpNotFound();
            }

            return this.View(eventDto);
        }

        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult EventAreaDetail(int? id)
        {
            var eventAreaDto = this.eventAreaService.GetEventAreaMap(id.Value);
            if (eventAreaDto == null)
            {
                return this.HttpNotFound();
            }

            return this.View(eventAreaDto);
        }

        [Authorize(Roles = "user, admin, event manager")]
        [HttpPost]
        public void AddToCart(int? seatId)
        {
            var identity = (ClaimsIdentity)this.User.Identity;
            var user = this.userService.FindByName(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
            this.orderService.AddToCart(seatId.Value, user);
        }

        private IEnumerable<EventViewModel> MapToEventViewModel(IEnumerable<Event> events)
        {
            List<EventViewModel> eventViewModelList = new List<EventViewModel>();
            foreach (Event @event in events)
            {
                EventViewModel eventViewModel = new EventViewModel
                {
                    Id = @event.Id,
                    Name = @event.Name,
                    Description = @event.Description,
                    BeginDate = @event.BeginDate,
                    EndDate = @event.EndDate,
                    BeginTime = @event.BeginDate,
                    EndTime = @event.EndDate,
                    CountFreeSeats = this.eventService.AvailibleSeatCount(@event.Id),
                    LayoutName = layoutService.GetLayout(@event.LayoutId).Name,
                };

                eventViewModelList.Add(eventViewModel);
            }

            return eventViewModelList;
        }
    }
}