// ****************************************************************************
// <copyright file="UserPasswordViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.UserProfile
{
    public class UserPasswordViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordMustBeFrom8To20symb")]
        [Display(Name = "OldPassword", ResourceType = typeof(Resources.TicketManagementResource))]
        public string OldPassword { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordMustBeFrom8To20symb")]
        [Display(Name = "Password", ResourceType = typeof(Resources.TicketManagementResource))]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordsDoNotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.TicketManagementResource))]
        public string PasswordConfirm { get; set; }
    }
}