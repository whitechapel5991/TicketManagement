// ****************************************************************************
// <copyright file="TicketManagementUserExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.AuthenticationApi.Services.Identity;

namespace TicketManagement.AuthenticationApi.Models.Extension
{
    public static class TicketManagementUserExtension
    {
        public static UserModel ConvertToUserModel(this TicketManagementUser user)
        {
            return new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Balance = user.Balance,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Language = user.Language,
                TimeZone = user.TimeZone,
            };
        }

        public static List<UserModel> ConvertToListUserModel(this List<TicketManagementUser> userList)
        {
            return userList.Select(tmUser => new UserModel
            {
                Id = tmUser.Id,
                UserName = tmUser.UserName,
                Email = tmUser.Email,
                Balance = tmUser.Balance,
                FirstName = tmUser.FirstName,
                Surname = tmUser.Surname,
                Language = tmUser.Language,
                TimeZone = tmUser.TimeZone,
            }).ToList();
        }
    }
}
