// ****************************************************************************
// <copyright file="UserService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Infrastructure;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<TicketManagementUser, int> userManager;

        private readonly RoleManager<Role, int> roleManager;

        public UserService(UserManager<TicketManagementUser, int> userManager, RoleManager<Role, int> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public ClaimsIdentity Authenticate(TicketManagementUser user)
        {
            ClaimsIdentity claim = default;
            var userTemp = this.userManager.Find(user.UserName, user.Password);

            if (userTemp != null)
            {
                var claims = this.userManager.GetClaims(userTemp.Id);

                claim = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public TicketManagementUser FindByEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public TicketManagementUser FindByUserNamePassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public TicketManagementUser FindByName(string userName)
        {
            throw new NotImplementedException();
        }

        public TicketManagementUser FindById(string userId)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Create(TicketManagementUser userDto)
        {
            TicketManagementUser user = this.userManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                userDto.EmailConfirmed = false;
                userDto.Balance = 0;
                userDto.PhoneNumberConfirmed = false;
                userDto.TwoFactorEnabled = false;
                userDto.LockoutEnabled = false;
                userDto.AccessFailedCount = 0;

                var result = this.userManager.Create(userDto, userDto.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), string.Empty);
                }

                var role = this.roleManager.FindByName("user");
                var userId = this.userManager.FindByEmail(userDto.Email).Id;

                this.userManager.AddToRole(userId, role.Name);

                return new OperationDetails(true, "Registration successfull", string.Empty);
            }
            else
            {
                return new OperationDetails(false, "User with this email exist", "Email");
            }
        }

        public OperationDetails Update(TicketManagementUser userDto)
        {
            throw new NotImplementedException();
        }

        public OperationDetails UpdatePassword(TicketManagementUser userDto)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Delete(TicketManagementUser user)
        {
            throw new NotImplementedException();
        }

        public List<TicketManagementUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
