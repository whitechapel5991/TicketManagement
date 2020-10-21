using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventDetailViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string LayoutName { get; set; }

        public List<EventAreaViewModel> EventAreas { get; set; }
    }
}