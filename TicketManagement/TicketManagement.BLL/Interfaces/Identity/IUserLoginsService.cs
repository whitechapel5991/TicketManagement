// ****************************************************************************
// <copyright file="IUserLoginsService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Interfaces.Identity
{
    public interface IUserLoginsService
    {
        void Add(UserLogin userLogin);

        UserLogin Find(UserLoginKey userLoginKey);

        IList<UserLogin> GetLoginsByUserId(int userId);

        void DeleteUserLogin(int userId, UserLogin userLogin);
    }
}
