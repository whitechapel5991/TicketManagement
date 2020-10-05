// ****************************************************************************
// <copyright file="IUserRoleRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IUserRoleRepository
    {
        void Add(int userId, string roleName);

        void Remove(int userId, string roleName);

        IEnumerable<string> GetRoleNamesByUserId(int userId);

        IEnumerable<TicketManagementUser> GetUsersByRoleName(string roleName);
    }
}
