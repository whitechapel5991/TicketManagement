﻿using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource),
            ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "EventName", ResourceType = typeof(Resources.TicketManagementResource))]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TicketManagementResource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.TicketManagementResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}