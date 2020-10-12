// ****************************************************************************
// <copyright file="IUserRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IUserRepository : IRepository<TicketManagementUser>
    {
        TicketManagementUser FindByNormalizedUserName(string normalizedUserName);

        TicketManagementUser FindByNormalizedEmail(string normalizedEmail);
    }
}
