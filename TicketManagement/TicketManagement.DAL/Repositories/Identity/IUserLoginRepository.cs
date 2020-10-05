// ****************************************************************************
// <copyright file="IUserLoginRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IUserLoginRepository : IRepository<UserLogin, UserLoginKey>
    {
        IEnumerable<UserLogin> FindByUserId(int userId);
    }
}
