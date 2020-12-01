// ****************************************************************************
// <copyright file="IAccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using TicketManagement.AuthenticationApi.Models;

namespace TicketManagement.AuthenticationApi.Services
{
    public interface IAccountService
    {
        Task<string> SignInAsync(string userName, string password);

        Task RegisterUserAsync(RegisterModel registerVm);

        string ValidateToken(string token);
    }
}
