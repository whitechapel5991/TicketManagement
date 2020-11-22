// ****************************************************************************
// <copyright file="TicketManagementUser.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNetCore.Identity;

namespace TicketManagement.AuthenticationApi.Services.Identity
{
    public sealed class TicketManagementUser : IdentityUser<int>
    {
        public TicketManagementUser()
        {
        }

        public TicketManagementUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public decimal Balance { get; set; }
    }
}