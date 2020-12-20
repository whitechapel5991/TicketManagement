// ****************************************************************************
// <copyright file="IAccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Interfaces
{
    public interface IAccountService
    {
        void SignOut();

        string SignIn(string userName, string password);

        void RegisterUser(RegisterViewModel registerVm);
    }
}
