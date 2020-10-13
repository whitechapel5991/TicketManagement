﻿using System.ComponentModel.DataAnnotations;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "IncorrectEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordMustBeFrom8To20symb")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordsDoNotMatch")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "UserNameRequired")]
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom3to30symb")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "FirstNameRequired")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "SurnameRequired")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "StringLenghtMessageFrom2to30symb")]
        public string Surname { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}