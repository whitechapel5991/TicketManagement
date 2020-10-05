﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.UserProfile
{
    public class PurchaseHistoryViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        public string EventName { get; set; }

        [StringLength(
            300,
            MinimumLength = 5,
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "StringLenghtMessageFrom5symb")]
        public string EventDescription { get; set; }

        [Range(typeof(decimal), "0.00", "1000000.00")]
        public decimal TicketCost { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePurchase { get; set; }
    }
}