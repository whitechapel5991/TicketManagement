// ****************************************************************************
// <copyright file="RoleRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    internal class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TicketManagementContext context)
            : base(context)
        {
        }

        public Role FindByName(string roleName)
        {
            return this.DbSet.First(x => x.Name == roleName);
        }
    }
}
