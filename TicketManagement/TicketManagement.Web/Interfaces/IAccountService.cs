using System.Threading.Tasks;
using TicketManagement.Web.Models.Account;

namespace TicketManagement.Web.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsUserEventManagerAsync(int userId);
        void SignOut();
        Task<int> SignInAsync(string userName, string password);
        Task RegisterUserAsync(RegisterViewModel registerVm);
    }
}
