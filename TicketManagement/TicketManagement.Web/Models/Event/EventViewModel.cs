// ****************************************************************************
// <copyright file="EventViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TicketManagement.Web.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Event", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "BeginDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "BeginTime", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime BeginTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "EndDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "EndTime", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(
            300,
            MinimumLength = 5,
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "StringLenghtMessageFrom5symb")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "CountFreeSeats", ResourceType = typeof(Resources.TicketManagementResource))]
        public int CountFreeSeats { get; set; }

        [Display(Name = "Layout", ResourceType = typeof(Resources.TicketManagementResource))]
        public string LayoutName { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }
    }
}