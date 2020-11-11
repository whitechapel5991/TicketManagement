// ****************************************************************************
// <copyright file="EventDetailViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventDetailViewModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "EventName", ResourceType = typeof(Resources.TicketManagementResource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "BeginDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime BeginDate { get; set; }

        [Required]
        [Display(Name = "EndDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(
            300,
            MinimumLength = 5,
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "StringLenghtMessageFrom5symb")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Layout", ResourceType = typeof(Resources.TicketManagementResource))]
        public string LayoutName { get; set; }

        public List<EventAreaViewModel> EventAreas { get; set; }
    }
}