using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<IdentityUser, int> userManager;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;

        public UserProfileService(UserManager<IdentityUser, int> userManager,
            IOrderService orderService,
            IEventSeatService eventSeatService,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.orderService = orderService;
            this.eventSeatService = eventSeatService;
        }

        public UserProfileViewModel GetUserProfileViewModel(string userName)
        {
            return this.MapToUserProfileViewModel(this.userManager.FindByName(userName));
        }

        public EditUserProfileViewModel GetEditUserProfileViewModel(string userName)
        {
            return this.MapToEditUserProfileViewModel(this.userManager.FindByName(userName));
        }

        public void Update(string userName, EditUserProfileViewModel userProfile)
        {
            var user = this.userManager.FindByName(userName);

            this.userManager.UpdateAsync(this.UpdateIdentityUserModel(user, userProfile));
        }

        public void ChangePassword(string userName, UserPasswordViewModel userPasswordModel)
        {
            var user = this.userManager.FindByName(userName);
            this.userManager.ChangePasswordAsync(user.Id, userPasswordModel.OldPassword, userPasswordModel.Password);
        }

        public BalanceViewModel GetBalanceViewModel(string userName)
        {
            var user = this.userManager.FindByName(userName);

            return new BalanceViewModel
            {
                Balance = user.Balance,
            };
        }

        public PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName)
        {
            return this.MapToPurchaseHistoryViewModel(this.orderService.GetHistoryOrdersByName(userName));
        }

        private PurchaseHistoryViewModel MapToPurchaseHistoryViewModel(List<Order> orderList)
        {
            var purchaseHistoryVm = new PurchaseHistoryViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            foreach (var order in orderList)
            {
                //var @event = this.eventSeatService.GetEventByEventSeatId(order.EventSeatId);

                //var orderVm = new OrderViewModel
                //{
                //    DatePurchase = order.Date,
                //    TicketCost = this.eventSeatService.GetSeatCost(order.EventSeatId),
                //    EventName = @event.Name,
                //    EventDescription = @event.Description,
                //};

                //purchaseHistoryVM.Orders.Add(orderVm);
            }

            return purchaseHistoryVm;
        }

        private IdentityUser UpdateIdentityUserModel(IdentityUser identityUser, EditUserProfileViewModel editUserProfile)
        {
            identityUser.FirstName = editUserProfile.FirstName;
            identityUser.Surname = editUserProfile.Surname;
            identityUser.Language = editUserProfile.Language.ToString();
            identityUser.TimeZone = editUserProfile.TimeZone;
            identityUser.Email = editUserProfile.Email;

            return identityUser;
        }

        private UserProfileViewModel MapToUserProfileViewModel(IdentityUser user)
        {
            return new UserProfileViewModel
            {
                Balance = user.Balance,
                Email = user.Email,
                FirstName = user.FirstName,
                Surname = user.Surname,
                UserName = user.UserName,
                Language = (Language)Enum.Parse(typeof(Language), user.Language, true),
                TimeZone = user.TimeZone,
            };
        }

        private EditUserProfileViewModel MapToEditUserProfileViewModel(IdentityUser user)
        {
            return new EditUserProfileViewModel
            {
                FirstName = user.FirstName,
                Surname = user.Surname,
                Language = (Language)Enum.Parse(typeof(Language), user.Language, true),
                TimeZone = user.TimeZone,
                Email = user.Email,
            };
        }
    }
}