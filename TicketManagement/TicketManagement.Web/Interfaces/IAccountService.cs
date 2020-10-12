using TicketManagement.Web.Models.Account;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Interfaces
{
    public interface IAccountService
    {
        bool IsUserEventManager(int userId);
        void SignOut();
        void SignIn(string userName, string password);
        void RegisterUser(IdentityUser user);
        IdentityUser MapIdentityUser(RegisterViewModel viewModel);
    }
}
