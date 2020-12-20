// ****************************************************************************
// <copyright file="AccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TicketManagement.Web.AuthenticationApi.Clients;
using TicketManagement.Web.AuthenticationApi.Models;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;
using TicketManagement.Web.WcfInfrastructure;

namespace TicketManagement.Web.Services
{
    internal class AccountService : IAccountService
    {
        private readonly AuthenticationClient authenticationClient;

        private readonly IAuthenticationManager authenticationManager;

        public AccountService(IAuthenticationManager authenticationManager)
        {
            this.authenticationClient = new AuthenticationClient();
            this.authenticationManager = authenticationManager;
        }

        public string SignIn(string userName, string password)
        {
            var token = this.authenticationClient.Login(userName, password);
            CredentialsContainer.UserName = userName;
            CredentialsContainer.Password = password;

            return token;
        }

        public void SignOut()
        {
            CredentialsContainer.UserName = default;
            CredentialsContainer.Password = default;
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void RegisterUser(RegisterViewModel registerVm)
        {
            var user = this.MapIdentityUser(registerVm);
            this.authenticationClient.Register(user);
        }

        private RegisterApiModel MapIdentityUser(RegisterViewModel viewModel)
        {
            return new RegisterApiModel
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                PasswordConfirm = viewModel.PasswordConfirm,
                UserName = viewModel.UserName,
                FirstName = viewModel.FirstName,
                Surname = viewModel.Surname,
                Language = viewModel.Language,
                TimeZone = viewModel.TimeZone,
            };
        }
    }
}