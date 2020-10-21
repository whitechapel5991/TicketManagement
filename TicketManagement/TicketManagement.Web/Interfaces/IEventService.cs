using System.Collections.Generic;
using TicketManagement.Web.Models.Event;

namespace TicketManagement.Web.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventViewModel> GetPublishEvents();
        void AddToCart(int seatId, int userId);

        EventDetailViewModel GetEventDetailViewModel(int eventId);

        EventAreaDetailViewModel GetEventAreaDetailViewModel(int eventAreaId);
    }
}
