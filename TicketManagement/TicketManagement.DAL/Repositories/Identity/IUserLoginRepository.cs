// ****************************************************************************
// <copyright file="IUserLoginRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    public interface IUserLoginRepository
    {
        UserLoginKey Create(UserLogin entity);

        void Update(UserLogin entity);

        void Delete(UserLoginKey id);

        UserLogin GetById(UserLoginKey id);

        IQueryable<UserLogin> GetAll();

        IEnumerable<UserLogin> FindByUserId(int userId);
    }
}
