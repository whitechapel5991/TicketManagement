// ****************************************************************************
// <copyright file="UserLoginService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.BLL.Services.Identity
{
    internal class UserLoginService : IUserLoginsService
    {
        private readonly IUserLoginRepository userLoginRepository;

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            this.userLoginRepository = userLoginRepository;
        }

        public void Add(UserLogin userLogin)
        {
            this.userLoginRepository.Create(userLogin);
        }

        public void DeleteUserLogin(int userId, UserLogin userLogin)
        {
            var userLogins = this.userLoginRepository.FindByUserId(userId);
            var userLoginForDeleting = userLogins.FirstOrDefault(x =>
                x.UserId == userId && x.LoginProvider == userLogin.LoginProvider && x.ProviderKey == userLogin.ProviderKey);

            if (userLoginForDeleting != null)
            {
                this.userLoginRepository.Delete(userLoginForDeleting);
            }
        }

        public UserLogin Find(UserLoginKey userLoginKey)
        {
            return this.userLoginRepository.GetById(userLoginKey);
        }

        public IEnumerable<UserLogin> GetLoginsByUserId(int userId)
        {
            return this.userLoginRepository.FindByUserId(userId).ToList();
        }
    }
}
