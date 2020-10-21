using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Constants;

namespace TicketManagement.Web.Models.Event
{
    public class EventSeatViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        public EventSeatState State { get; set; }
    }
}