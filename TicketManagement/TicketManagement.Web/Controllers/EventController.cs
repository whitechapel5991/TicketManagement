using System.Security.Claims;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.Web.Filters;
using IEventServiceWeb = TicketManagement.Web.Interfaces.IEventService;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventAreaService eventAreaService;
        private readonly IEventServiceWeb eventWebService;

        public EventController(
            IEventServiceWeb eventWebService,
            IEventService eventService,
            IEventAreaService eventAreaService
        )
        {
            this.eventWebService = eventWebService;
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult Events()
        {
            return this.View(this.eventWebService.GetPublishEvents());
        }

        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult EventDetail(int id)
        {
            var eventDto = this.eventService.GetEventMap(id);
            if (eventDto == default)
            {
                return this.HttpNotFound();
            }

            return this.View(eventDto);
        }

        [Authorize(Roles = "user, admin, event manager")]
        public ActionResult EventAreaDetail(int id)
        {
            var eventAreaDto = this.eventAreaService.GetEventAreaMap(id);
            if (eventAreaDto == null)
            {
                return this.HttpNotFound();
            }

            return this.View(eventAreaDto);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public void AddToCart(int seatId)
        {
            this.eventWebService.AddToCart(seatId, (ClaimsIdentity)this.User.Identity);
        }
    }
}