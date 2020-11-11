// ****************************************************************************
// <copyright file="RegisterViewModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources.TicketManagementResource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "IncorrectEmail")]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordMustBeFrom8To20symb")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordsDoNotMatch")]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "UserNameRequired")]
        [Display(Name = "UserName", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        public string UserName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        public string FirstName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "SurnameRequired")]
        [Display(Name = "Surname", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Language", ResourceType = typeof(Resources.TicketManagementResource))]
        public Language Language { get; set; }

        [Required]
        [Display(Name = "TimeZone", ResourceType = typeof(Resources.TicketManagementResource))]
        public string TimeZone { get; set; }
    }
}