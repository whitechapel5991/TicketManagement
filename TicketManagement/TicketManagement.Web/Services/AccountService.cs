﻿// ****************************************************************************
// <copyright file="AccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ApplicationUserManager userManager;

        private readonly IAuthenticationManager authenticationManager;

        public AccountService(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        public async Task<int> SignInAsync(string userName, string password)
        {
            var user = await this.userManager.FindAsync(userName, password);

            if (user == null)
            {
                throw new UserNameOrPasswordWrongException(Resources.TicketManagementResource.WrongCredentials);
            }

            var claimIdentity = await this.userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            this.authenticationManager.SignIn(
                new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7),
                }, claimIdentity);

            return user.Id;
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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