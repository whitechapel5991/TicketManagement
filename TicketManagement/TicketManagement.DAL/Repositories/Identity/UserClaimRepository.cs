// ****************************************************************************
// <copyright file="UserClaimRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    internal class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(IGenerateDbContext contextGenerator)
            : base(contextGenerator)
        {
        }

        public IEnumerable<UserClaim> GetByUserId(int userId)
        {
            return this.ContextGenerator.GenerateNewContext().Set<UserClaim>().Where(x => x.UserId == userId);
        }

        public IEnumerable<TicketManagementUser> GetUsersForClaim(string claimType, string claimValue)
        {
            var userIdList = this.ContextGenerator.GenerateNewContext().Set<UserClaim>().Where(x => x.ClaimType == claimType && x.ClaimValue == claimValue).Select(x => x.UserId);
            return this.ContextGenerator.GenerateNewContext().Users.Join(userIdList, user => user.Id, id => id, (user, id) => user);
        }
    }
}
