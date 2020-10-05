// ****************************************************************************
// <copyright file="UserRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    internal class UserRepository : Repository<TicketManagementUser>, IUserRepository
    {
        public UserRepository(TicketManagementContext context)
            : base(context)
        {
        }

        public TicketManagementUser FindByNormalizedUserName(string normalizedUserName)
        {
            return this.DbSet.First(x => x.UserName == normalizedUserName);
        }

        public TicketManagementUser FindByNormalizedEmail(string normalizedEmail)
        {
            return this.DbSet.First(x => x.Email == normalizedEmail);
        }
    }
}
