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
        [AjaxContentUrl]
        public ActionResult Events()
        {
            return this.PartialView(this.eventService.GetPublishEvents());
        }

        [HttpGet]
        [Authorize()]
        [AjaxContentUrl]
        public ActionResult EventDetail(int eventId)
        {
            var eventAreaDetailVm = this.eventService.GetEventDetailViewModel(eventId);
            if (eventAreaDetailVm == default)
            {
                return this.HttpNotFound();
            }

            return this.PartialView(eventAreaDetailVm);
        }

        [HttpGet]
        [Authorize()]
        [AjaxContentUrl]
        public ActionResult EventAreaDetail(int eventAreaId)
        {
            var eventAreaDto = this.eventService.GetEventAreaDetailViewModel(eventAreaId);
            if (eventAreaDto == null)
            {
                return this.HttpNotFound();
            }

            return this.PartialView(eventAreaDto);
        }

        [Authorize()]
        [HttpPost]
        public ActionResult AddToCart(int seatId)
        {
            this.eventService.AddToCart(seatId, this.User.Identity.GetUserId<int>());
            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}