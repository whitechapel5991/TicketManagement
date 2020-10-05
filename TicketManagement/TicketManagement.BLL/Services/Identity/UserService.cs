// ****************************************************************************
// <copyright file="UserService.cs" company="EPAM Systems">
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
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        private readonly IUserRoleRepository userRoleRepository;

        public UserService(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
        }

        public void Add(TicketManagementUser userDto)
        {
            this.userRepository.Create(userDto);
        }

        public void AddRole(int userId, string roleName)
        {
            this.userRoleRepository.Add(userId, roleName);
        }

        public void Delete(TicketManagementUser user)
        {
            this.userRepository.Delete(user.Id);
        }

        public void DeleteRole(int userId, string roleName)
        {
            this.userRoleRepository.Remove(userId, roleName);
        }

        public TicketManagementUser FindById(int userId)
        {
            return this.userRepository.GetById(userId);
        }

        public TicketManagementUser FindByName(string userName)
        {
            return this.userRepository.FindByNormalizedUserName(userName);
        }

        public string GetPasswordHash(int userId)
        {
            return this.userRepository.GetById(userId).PasswordHash;
        }

        public IList<string> GetRoles(int userId)
        {
            return this.userRoleRepository.GetRoleNamesByUserId(userId).ToList();
        }

        public string GetSecurityStamp(int userId)
        {
            return this.userRepository.GetById(userId).SecurityStamp;
        }

        public bool HasPassword(int userId)
        {
            return string.IsNullOrWhiteSpace(this.userRepository.GetById(userId).PasswordHash);
        }

        public bool IsRole(int userId, string roleName)
        {
            return this.userRoleRepository.GetRoleNamesByUserId(userId).Any(x => x.Equals(roleName));
        }

        public void SetPassword(int userId, string password)
        {
            var user = this.userRepository.GetById(userId);
            user.PasswordHash = password;
            this.userRepository.Update(user);
        }

        public void SetSecurityStamp(int userId, string stamp)
        {
            var user = this.userRepository.GetById(userId);
            user.SecurityStamp = stamp;
            this.userRepository.Update(user);
        }

        public void Update(TicketManagementUser userDto)
        {
            this.userRepository.Update(userDto);
        }

        public void IncreaseBalance(decimal money, string userName)
        {
            TicketManagementUser user = this.userRepository.FindByNormalizedUserName(userName);
            user.Balance += money;
            this.userRepository.Update(user);
        }
    }
}
