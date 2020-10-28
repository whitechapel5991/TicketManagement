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
        public UserRepository(IGenerateDbContext contextGenerator)
            : base(contextGenerator)
        {
        }

        public TicketManagementUser FindByNormalizedUserName(string normalizedUserName)
        {
            return this.ContextGenerator.GenerateNewContext().Set<TicketManagementUser>().FirstOrDefault(x => x.UserName == normalizedUserName);
        }

        public TicketManagementUser FindByNormalizedEmail(string normalizedEmail)
        {
            return this.ContextGenerator.GenerateNewContext().Set<TicketManagementUser>().First(x => x.Email == normalizedEmail);
        }
    }
}
