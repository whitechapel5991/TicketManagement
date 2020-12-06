// ****************************************************************************
// <copyright file="AccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using TicketManagement.Web.AuthenticationApi.Clients;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ApplicationUserManager userManager;

        public AccountService(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        public string SignIn(string userName, string password)
        {
            var apiClient = new AuthenticationClient();
            var token = apiClient.Login(userName, password);

            return token;
        }

        public void SignOut()
        {
        }

        public async Task RegisterUserAsync(RegisterViewModel registerVm)
        {
            var user = this.MapIdentityUser(registerVm);
            var registerResult = await this.userManager.CreateAsync(user, registerVm.Password);
            if (!registerResult.Succeeded)
            {
                throw new RegisterUserWrongDataException(string.Join(", ", registerResult.Errors));
            }
        }

        private IdentityUser MapIdentityUser(RegisterViewModel viewModel)
        {
            return new IdentityUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                FirstName = viewModel.FirstName,
                Surname = viewModel.Surname,
                Language = viewModel.Language.ToString(),
                TimeZone = viewModel.TimeZone,
            };
        }
    }
}