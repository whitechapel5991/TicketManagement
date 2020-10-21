using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [RedirectExceptionFilter]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(
            IEventService eventService
        )
        {
            this.eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Events()
        {
            return this.View(this.eventService.GetPublishEvents());
        }

        [Authorize()]
        public ActionResult EventDetail(int eventId)
        {
            var eventAreaDetailVm = this.eventService.GetEventDetailViewModel(eventId);
            if (eventAreaDetailVm == default)
            {
                return this.HttpNotFound();
            }

            return this.View(eventAreaDetailVm);
        }

        [Authorize()]
        public ActionResult EventAreaDetail(int eventAreaId)
        {
            var eventAreaDto = this.eventService.GetEventAreaDetailViewModel(eventAreaId);
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
            this.eventService.AddToCart(seatId, this.User.Identity.GetUserId<int>());
        }
    }
}