// ****************************************************************************
// <copyright file="EditUserProfileViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.UserProfile
{
    public class EditUserProfileViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.TicketManagementResource))]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        [Display(Name = "Surname", ResourceType = typeof(Resources.TicketManagementResource))]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Language", ResourceType = typeof(Resources.TicketManagementResource))]
        public Language Language { get; set; }

        [Required]
        [Display(Name = "TimeZone", ResourceType = typeof(Resources.TicketManagementResource))]
        public string TimeZone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.TicketManagementResource))]
        public string Email { get; set; }
    }
}