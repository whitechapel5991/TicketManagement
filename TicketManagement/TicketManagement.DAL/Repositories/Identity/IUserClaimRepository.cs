// ****************************************************************************
// <copyright file="IUserClaimRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IUserClaimRepository : IRepository<UserClaim, int>
    {
        IEnumerable<UserClaim> GetByUserId(int userId);

        IEnumerable<TicketManagementUser> GetUsersForClaim(string claimType, string claimValue);
    }
}
