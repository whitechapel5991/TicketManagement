// ****************************************************************************
// <copyright file="IUserService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Interfaces.Identity
{
    public interface IUserService
    {
        TicketManagementUser FindByName(string userName);

        TicketManagementUser FindByEmail(string email);

        TicketManagementUser FindById(int userId);

        int Add(TicketManagementUser userDto);

        void Update(TicketManagementUser userDto);

        void Delete(TicketManagementUser user);

        void IncreaseBalance(decimal money, string userName);

        void AddRole(int userId, string roleName);

        IList<string> GetRoles(int userId);

        bool IsRole(int userId, string roleName);

        void DeleteRole(int userId, string roleName);

        string GetPasswordHash(int userId);

        bool HasPassword(int userId);

        void SetPassword(int userId, string password);

        string GetSecurityStamp(int userId);

        void SetSecurityStamp(int userId, string stamp);
    }
}
