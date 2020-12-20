// ****************************************************************************
// <copyright file="UserModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.AuthenticationApi.Services.Identity;

namespace TicketManagement.AuthenticationApi.Models.Extension
{
    public static class UserModelExtension
    {
        public static TicketManagementUser ConvertToTicketManagementUser(this UserModel user)
        {
            return new TicketManagementUser
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
    }
}
