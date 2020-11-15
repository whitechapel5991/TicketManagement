// ****************************************************************************
// <copyright file="OrderViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Cart
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        [Display(Name = "EventName", ResourceType = typeof(Resources.TicketManagementResource))]
        public string EventName { get; set; }

        [StringLength(
            300,
            MinimumLength = 5,
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "StringLenghtMessageFrom5symb")]
        [Display(Name = "Description", ResourceType = typeof(Resources.TicketManagementResource))]
        public string EventDescription { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000000.00)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Cost", ResourceType = typeof(Resources.TicketManagementResource))]
        public decimal TicketCost { get; set; }
    }
}