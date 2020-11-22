// ****************************************************************************
// <copyright file="IdentityRole.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNetCore.Identity;

namespace TicketManagement.AuthenticationApi.Services.Identity
{
    public sealed class IdentityRole : IdentityRole<int>
    {
        public IdentityRole()
        {
        }

        public IdentityRole(string name)
            : this()
        {
            this.Name = name;
        }

        public IdentityRole(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}