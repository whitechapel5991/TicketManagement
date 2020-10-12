using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Constants.Extension;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser, int> userManager;
        private readonly IAuthenticationManager authenticationManager;


        public AccountService(UserManager<IdentityUser, int> userManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        public void SignIn(string userName, string password)
        {
            var user = this.userManager.FindAsync(userName, password).Result;

            if (user == null)
            {
                throw new UserNameOrPasswordWrongException(Resources.TicketManagementResource.WrongCredentials);
            }

            var claimIdentity = userManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie).Result;

            this.authenticationManager.SignOut();
            this.authenticationManager.SignIn(claimIdentity);
        }

        public bool IsUserEventManager(int userId)
        {
            return this.userManager.GetRolesAsync(userId).Result
                .Any(x => x == Roles.UserManager.GetStringValue());
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void RegisterUser(IdentityUser user)
        {
            var registerResult = this.userManager.Create(user);
            if (!registerResult.Succeeded)
            {
                throw new RegisterUserWrongDataException(string.Join(", ", registerResult.Errors));
            }
        }

        public IdentityUser MapIdentityUser(RegisterViewModel viewModel)
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