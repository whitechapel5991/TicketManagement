using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Models.Cart
{
    public class CartViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string EventName { get; set; }

        [StringLength(
            300,
            MinimumLength = 5)]
        public string EventDescription { get; set; }

        [Range(typeof(decimal), "0.00", "1000000.00")]
        public decimal EventCost { get; set; }
    }
}