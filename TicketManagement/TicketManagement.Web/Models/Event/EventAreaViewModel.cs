// ****************************************************************************
// <copyright file="EventAreaViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Event
{
    public class EventAreaViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Resources.TicketManagementResource))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "CoordinateX", ResourceType = typeof(Resources.TicketManagementResource))]
        public int CoordinateX { get; set; }

        [Required]
        [Display(Name = "CoordinateY", ResourceType = typeof(Resources.TicketManagementResource))]
        public int CoordinateY { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resources.TicketManagementResource))]
        public decimal Price { get; set; }
    }
}