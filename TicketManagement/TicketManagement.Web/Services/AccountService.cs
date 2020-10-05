using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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

        public ClaimsIdentity GetUserClaimIdentity(string userName, string password)
        {
            var user = this.userManager.FindAsync(userName, password).Result;
            return userManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie).Result;
        }

        public void Authenticate(ClaimsIdentity claimIdentity)
        {
            this.authenticationManager.SignOut();
            this.authenticationManager.SignIn(claimIdentity);
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public bool IsRoleInClaimIdentity(ClaimsIdentity claimIdentity, string roleName)
        {
            return claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == roleName;
        }

        public IdentityResult RegisterUser(IdentityUser user)
        {
            return this.userManager.Create(user);
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