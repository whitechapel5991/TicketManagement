// ****************************************************************************
// <copyright file="UserStore.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.AuthenticationApi.Services.Identity
{
    public class UserStore : IUserLoginStore<TicketManagementUser>,
        IUserPasswordStore<TicketManagementUser>,
        IUserEmailStore<TicketManagementUser>,
        IUserRoleStore<TicketManagementUser>
    {
        private readonly BLL.Interfaces.Identity.IUserService userService;
        private readonly IUserLoginsService userLoginService;

        public UserStore(
            BLL.Interfaces.Identity.IUserService userService,
            IUserLoginsService userLoginService)
        {
            this.userService = userService;
            this.userLoginService = userLoginService;
        }

        public Task<IdentityResult> CreateAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userService.Add(this.GetTicketManagementUser(user));
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userService.Delete(this.GetTicketManagementUser(user));
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<TicketManagementUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = userId.ToLower();
            }

            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return Task.FromResult(this.GetIdentityUser(this.userService.FindById(Convert.ToInt32(userId))));
        }

        public Task<TicketManagementUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.GetIdentityUser(this.userService.FindByName(normalizedUserName)));
        }

        public Task<string> GetNormalizedUserNameAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TicketManagementUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TicketManagementUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.UserName = userName;
            this.userService.Update(this.GetTicketManagementUser(user));
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var dbUser = this.userService.FindById(user.Id);
            var userForUpdate = this.GetTicketManagementUser(user);
            userForUpdate.Password = user.Password ?? dbUser.Password;
            userForUpdate.EmailConfirmed = dbUser.EmailConfirmed;
            userForUpdate.PasswordHash = user.PasswordHash ?? dbUser.PasswordHash;
            userForUpdate.SecurityStamp = user.SecurityStamp ?? dbUser.SecurityStamp;
            this.userService.Update(userForUpdate);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            // Autofac manage the lifecycle
        }

        public Task AddLoginAsync(TicketManagementUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userLoginService.Add(this.GetUserLogin(user, login));
            return Task.CompletedTask;
        }

        public Task<TicketManagementUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userLoginKey = this.userLoginService.Find(this.GetUserLoginKey(new UserLoginInfo(loginProvider, providerKey, string.Empty)));
            return Task.FromResult(this.GetIdentityUser(this.userService.FindById(userLoginKey.UserId)));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult((IList<UserLoginInfo>)this.userLoginService.GetLoginsByUserId(user.Id).Select(this.GetUserLoginInfo));
        }

        public Task RemoveLoginAsync(TicketManagementUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userLoginService.DeleteUserLogin(user.Id, this.GetUserLogin(user, new UserLoginInfo(loginProvider, providerKey, string.Empty)));
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.userService.GetPasswordHash(user.Id));
        }

        public Task<bool> HasPasswordAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.userService.HasPassword(user.Id));
        }

        public Task SetPasswordHashAsync(TicketManagementUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<TicketManagementUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.GetIdentityUser(this.userService.FindByEmail(normalizedEmail)));
        }

        public Task<string> GetEmailAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(TicketManagementUser user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TicketManagementUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(TicketManagementUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task AddToRoleAsync(TicketManagementUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userService.AddRole(user.Id, roleName);
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(TicketManagementUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.userService.GetRoles(user.Id));
        }

        public Task<IList<TicketManagementUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(TicketManagementUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.userService.IsRole(user.Id, roleName));
        }

        public Task RemoveFromRoleAsync(TicketManagementUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.userService.DeleteRole(user.Id, roleName);
            return Task.CompletedTask;
        }

        private DAL.Models.Identity.TicketManagementUser GetTicketManagementUser(TicketManagementUser identityUser) => new DAL.Models.Identity.TicketManagementUser()
        {
            Id = identityUser.Id,
            UserName = identityUser.UserName,
            PasswordHash = identityUser.PasswordHash,
            SecurityStamp = identityUser.SecurityStamp,
            Password = identityUser.Password,
            Email = identityUser.Email,
            FirstName = identityUser.FirstName,
            Language = identityUser.Language,
            Surname = identityUser.Surname,
            TimeZone = identityUser.TimeZone,
            Balance = identityUser.Balance,
        };

        private TicketManagementUser GetIdentityUser(DAL.Models.Identity.TicketManagementUser ticketManagementUser)
        {
            if (ticketManagementUser == default)
            {
                return default;
            }

            return new TicketManagementUser()
            {
                Id = ticketManagementUser.Id,
                UserName = ticketManagementUser.UserName,
                PasswordHash = ticketManagementUser.PasswordHash,
                SecurityStamp = ticketManagementUser.SecurityStamp,
                Password = ticketManagementUser.Password,
                Email = ticketManagementUser.Email,
                FirstName = ticketManagementUser.FirstName,
                Language = ticketManagementUser.Language,
                Surname = ticketManagementUser.Surname,
                TimeZone = ticketManagementUser.TimeZone,
                Balance = ticketManagementUser.Balance,
            };
        }

        private UserLogin GetUserLogin(TicketManagementUser user, UserLoginInfo userLoginInfo) => new UserLogin
        {
            UserId = user.Id,
            LoginProvider = userLoginInfo.LoginProvider,
            ProviderKey = userLoginInfo.ProviderKey,
        };

        private UserLoginInfo GetUserLoginInfo(UserLogin userLogin) =>
            new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey, string.Empty);

        private UserLoginKey GetUserLoginKey(UserLoginInfo userLoginInfo) => new UserLoginKey
        {
            ProviderKey = userLoginInfo.ProviderKey,
            LoginProvider = userLoginInfo.LoginProvider,
        };
    }
}