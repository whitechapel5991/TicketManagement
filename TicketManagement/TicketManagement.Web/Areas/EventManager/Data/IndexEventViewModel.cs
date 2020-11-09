// ****************************************************************************
// <copyright file="IndexEventViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TicketManagement.Web.Areas.EventManager.Data
{
    public class IndexEventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "EventName", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "BeginDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "BeginTime", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime BeginTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "EndDate", ResourceType = typeof(Resources.TicketManagementResource))]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
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

        [Display(Name = "LayoutId", ResourceType = typeof(Resources.TicketManagementResource))]
        public int LayoutId { get; set; }

        [Display(Name = "Layout", ResourceType = typeof(Resources.TicketManagementResource))]
        public string LayoutName { get; set; }

        [Display(Name = "Published", ResourceType = typeof(Resources.TicketManagementResource))]
        public bool Published { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        public DateTime GetBeginDate()
        {
            return this.BeginDate.AddHours(this.BeginTime.Hour)
                .AddMinutes(this.BeginTime.Minute)
                .AddSeconds(this.BeginTime.Second);
        }

        public DateTime GetEndDate()
        {
            return this.EndDate.AddHours(this.EndTime.Hour)
                .AddMinutes(this.EndTime.Minute)
                .AddSeconds(this.EndTime.Second);
        }
    }
}