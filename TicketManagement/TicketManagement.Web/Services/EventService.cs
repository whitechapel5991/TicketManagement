using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Models.Event;
using TicketManagement.Web.Services.Identity;
using IEventServiceWeb = TicketManagement.Web.Interfaces.IEventService;

namespace TicketManagement.Web.Services
{
    internal class EventService : IEventServiceWeb
    {
        private readonly UserManager<IdentityUser, int> userManager;
        private readonly IEventService eventService;
        private readonly ILayoutService layoutService;
        private readonly IOrderService orderService;

        public EventService(
            IEventService eventService,
            ILayoutService layoutService,
            UserManager<IdentityUser, int> userManager,
            IOrderService orderService)
        {
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.userManager = userManager;
            this.orderService = orderService;
        }

        public IEnumerable<EventViewModel> GetPublishEvents()
        {
            return this.MapToEventViewModel(this.eventService.GetPublishEvents());
        }

        public void AddToCart(int seatId, ClaimsIdentity claimIdentity)
        {
            var user = this.userManager.FindByName(claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
            this.orderService.AddToCart(seatId, user.Id);
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