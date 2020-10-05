using System.Security.Claims;
using Microsoft.AspNet.Identity;
using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Interfaces
{
    public interface IAccountService
    {
        ClaimsIdentity GetUserClaimIdentity(string userName, string password);
        void Authenticate(ClaimsIdentity claimIdentity);
        bool IsRoleInClaimIdentity(ClaimsIdentity claimIdentity, string roleName);
        void SignOut();
        IdentityResult RegisterUser(IdentityUser user);
        IdentityUser MapIdentityUser(RegisterViewModel viewModel);
    }
}
