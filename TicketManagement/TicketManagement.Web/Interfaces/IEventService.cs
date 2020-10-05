using System.Collections.Generic;
using System.Security.Claims;
using TicketManagement.Web.Models.Event;

namespace TicketManagement.Web.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventViewModel> GetPublishEvents();
        void AddToCart(int seatId, ClaimsIdentity claimIDentity);
    }
}
