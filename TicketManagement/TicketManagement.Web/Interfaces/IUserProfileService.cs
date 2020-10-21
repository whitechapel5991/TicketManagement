using System.Threading.Tasks;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Interfaces
{
    public interface IUserProfileService
    {
        UserProfileViewModel GetUserProfileViewModel(string userName);
        EditUserProfileViewModel GetEditUserProfileViewModel(string userName);
        Task UpdateAsync(string userName, EditUserProfileViewModel userProfile);
        Task ChangePasswordAsync(string userName, UserPasswordViewModel userPasswordModel);
        Task<BalanceViewModel> GetBalanceViewModelAsync(string userName);
        PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName);
    }
}
