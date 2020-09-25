// ****************************************************************************
// <copyright file="RoleRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNet.Identity.EntityFramework;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    public class RoleRepository : RoleStore<Role, int, UserRole>
    {
        public RoleRepository(TicketManagementContext dbContext)
            : base(dbContext)
        {
        }
    }
}
