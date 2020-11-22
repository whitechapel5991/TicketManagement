// ****************************************************************************
// <copyright file="RegisterModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.AuthenticationApi.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must has symbols from 8 to 20")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwaords don't match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "UserName must has symbols from 3 to 30")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "FirstName must has symbols from 2 to 30")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "SurName is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname must has symbols from 2 to 30")]
        public string Surname { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}
