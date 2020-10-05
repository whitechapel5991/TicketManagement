using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Interfaces
{
    public interface IUserProfileService
    {
        UserProfileViewModel GetUserProfileViewModel(string userName);
        EditUserProfileViewModel GetEditUserProfileViewModel(string userName);
        void Update(string userName, EditUserProfileViewModel userProfile);
        void ChangePassword(string userName, UserPasswordViewModel userPasswordModel);
        BalanceViewModel GetBalanceViewModel(string userName);
        PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName);
    }
}
