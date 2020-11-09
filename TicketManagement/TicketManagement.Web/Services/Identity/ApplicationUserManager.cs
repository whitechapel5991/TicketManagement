// ****************************************************************************
// <copyright file="ApplicationUserManager.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity;

namespace TicketManagement.Web.Services.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser, int>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, int> store)
            : base(store)
        {
        }
    }
}