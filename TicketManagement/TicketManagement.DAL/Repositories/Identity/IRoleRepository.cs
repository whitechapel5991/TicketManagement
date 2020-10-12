// ****************************************************************************
// <copyright file="IRoleRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role FindByName(string roleName);
    }
}
