// ****************************************************************************
// <copyright file="UserRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity.EntityFramework;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    public class UserRepository : UserStore<TicketManagementUser, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public UserRepository(TicketManagementContext dbContext)
            : base(dbContext)
        {
        }
    }
}
