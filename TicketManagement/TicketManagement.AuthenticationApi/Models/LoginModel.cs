// ****************************************************************************
// <copyright file="LoginModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.AuthenticationApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "UserName must has symbols from 3 to 30")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must has symbols from 8 to 20")]
        public string Password { get; set; }
    }
}
