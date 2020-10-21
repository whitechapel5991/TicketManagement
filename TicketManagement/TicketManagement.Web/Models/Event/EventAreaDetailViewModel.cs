using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventAreaDetailViewModel
    {
        [Required]
        public EventAreaViewModel EventArea { get; set; }

        public int EventId { get; set; }

        public List<EventSeatViewModel> EventSeats { get; set; }
    }
}