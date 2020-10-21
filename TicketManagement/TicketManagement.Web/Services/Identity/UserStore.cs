using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.Web.Services.Identity
{
    public class UserStore : IUserLoginStore<IdentityUser, int>, IUserClaimStore<IdentityUser, int>, IUserRoleStore<IdentityUser, int>, IUserPasswordStore<IdentityUser, int>, IUserSecurityStampStore<IdentityUser, int>, IUserStore<IdentityUser, int>, IDisposable
    {
        private readonly IUserService userService;
        private readonly IUserClaimService userClaimService;
        private readonly IUserLoginsService userLoginService;

        public UserStore(IUserService userService,
            IUserClaimService userClaimService,
            IUserLoginsService userLoginService)
        {
            this.userService = userService;
            this.userClaimService = userClaimService;
            this.userLoginService = userLoginService;
        }

        #region IUserStore<IdentityUser, int> Members

        public Task CreateAsync(IdentityUser user)
        {
            this.userService.Add(this.GetTicketManagementUser(user));
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IdentityUser user)
        {
            this.userService.Delete(this.GetTicketManagementUser(user));
            return Task.CompletedTask;
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            return Task.FromResult(this.GetIdentityUser(this.userService.FindById(userId)));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(this.GetIdentityUser(this.userService.FindByName(userName)));
        }

        public Task UpdateAsync(IdentityUser user)
        {
            this.userService.Update(this.GetTicketManagementUser(user));
            return Task.CompletedTask;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Autofac manage the lifecycle
        }

        #endregion

        #region IUserClaimStore<IdentityUser, int> Members

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            this.userClaimService.Add(user.Id, claim);
            return Task.CompletedTask;
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            return Task.FromResult(this.userClaimService.GetClaims(user.Id));
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            this.userClaimService.Remove(user.Id, claim);
            return Task.CompletedTask;
        }

        #endregion

        #region IUserLoginStore<IdentityUser, int> Members

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            this.userLoginService.Add(this.GetUserLogin(user, login));
            return Task.CompletedTask;
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            var userLoginKey = this.userLoginService.Find(this.GetUserLoginKey(login));
            return Task.FromResult(this.GetIdentityUser(this.userService.FindById(userLoginKey.UserId)));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            return Task.FromResult((IList<UserLoginInfo>)this.userLoginService.GetLoginsByUserId(user.Id).Select(this.GetUserLoginInfo));

        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            this.userLoginService.DeleteUserLogin(user.Id, this.GetUserLogin(user, login));
            return Task.CompletedTask;
        }
        #endregion

        #region IUserRoleStore<IdentityUser, int> Members
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            this.userService.AddRole(user.Id, roleName);
            return Task.CompletedTask;

        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return Task.FromResult(this.userService.GetRoles(user.Id));
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return Task.FromResult(this.userService.IsRole(user.Id, roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            this.userService.DeleteRole(user.Id, roleName);
            return Task.CompletedTask;
        }
        #endregion

        #region IUserPasswordStore<IdentityUser, int> Members

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(this.userService.GetPasswordHash(user.Id));
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(this.userService.HasPassword(user.Id));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            this.userService.SetPassword(user.Id, passwordHash);
            return Task.CompletedTask;
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUser, int> Members

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            return Task.FromResult(this.userService.GetSecurityStamp(user.Id));
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            this.userService.SetSecurityStamp(user.Id, stamp);
            return Task.CompletedTask;
        }
        #endregion

        #region Mappers

        private TicketManagementUser GetTicketManagementUser(IdentityUser identityUser) => new TicketManagementUser()
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

        private IdentityUser GetIdentityUser(TicketManagementUser ticketManagementUser) => new IdentityUser()
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

        private UserLogin GetUserLogin(IdentityUser user, UserLoginInfo userLoginInfo) => new UserLogin
        {
            UserId = user.Id,
            LoginProvider = userLoginInfo.LoginProvider,
            ProviderKey = userLoginInfo.ProviderKey,
        };

        private UserLoginInfo GetUserLoginInfo(UserLogin userLogin) =>
            new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey);

        private UserLoginKey GetUserLoginKey(UserLoginInfo userLoginInfo) => new UserLoginKey
        {
            ProviderKey = userLoginInfo.ProviderKey,
            LoginProvider = userLoginInfo.LoginProvider,
        };

        #endregion
    }
}