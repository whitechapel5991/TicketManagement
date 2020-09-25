// ****************************************************************************
// <copyright file="IUserService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Security.Claims;
using TicketManagement.BLL.Infrastructure;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Interfaces
{
    public interface IUserService
    {
        ClaimsIdentity Authenticate(TicketManagementUser user);

        TicketManagementUser FindByEmailPassword(string email, string password);

        TicketManagementUser FindByUserNamePassword(string userName, string password);

        TicketManagementUser FindByName(string userName);

        TicketManagementUser FindById(string userId);

        OperationDetails Create(TicketManagementUser userDto);

        OperationDetails Update(TicketManagementUser userDto);

        OperationDetails UpdatePassword(TicketManagementUser userDto);

        OperationDetails Delete(TicketManagementUser user);

        List<TicketManagementUser> GetAllUsers();
    }
}
