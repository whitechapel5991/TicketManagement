using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventAreaViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CoordinateX { get; set; }

        [Required]
        public int CoordinateY { get; set; }

        public decimal Price { get; set; }
    }
}