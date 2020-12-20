// ****************************************************************************
// <copyright file="IUserProfileService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using TicketManagement.Web.Models.UserProfile;

namespace TicketManagement.Web.Interfaces
{
    public interface IUserProfileService
    {
        UserProfileViewModel GetUserProfileViewModel(string userName);

        EditUserProfileViewModel GetEditUserProfileViewModel(string userName);

        void Update(string userName, EditUserProfileViewModel userProfile);

        void ChangePassword(string userName, UserPasswordViewModel userPasswordModel);

        BalanceViewModel GetBalanceViewModelAsync(string userName);

        PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName);

        void IncreaseBalance(decimal balance, string userName);
    }
}
