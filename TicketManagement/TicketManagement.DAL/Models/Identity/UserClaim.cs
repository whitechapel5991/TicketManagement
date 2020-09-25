// ****************************************************************************
// <copyright file="UserClaim.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketManagement.DAL.Models.Identity
{
    public class UserClaim : IdentityUserClaim<int>
    {
    }
}
