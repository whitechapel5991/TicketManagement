using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> SignInAsync(string userName, string password)
        {
            var user = await this.userManager.FindAsync(userName, password);

            if (user == null)
            {
                throw new UserNameOrPasswordWrongException(Resources.TicketManagementResource.WrongCredentials);
            }

            var claimIdentity = await userManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            this.authenticationManager.SignOut();
            this.authenticationManager.SignIn(claimIdentity);
            return user.Id;
        }

        public async Task<bool> IsUserEventManagerAsync(int userId)
        {
            var roles = await this.userManager.GetRolesAsync(userId);
            return roles.Any(x => x == Roles.UserManager.GetStringValue());
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task RegisterUserAsync(RegisterViewModel registerVm)
        {
            var user = this.MapIdentityUser(registerVm);
            var registerResult = await this.userManager.CreateAsync(user);
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