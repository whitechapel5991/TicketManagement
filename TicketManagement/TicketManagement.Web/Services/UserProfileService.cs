// ****************************************************************************
// <copyright file="UserProfileService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Constants;
using TicketManagement.Web.Exceptions.UserProfile;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationUserManager userManager;
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;
        private readonly IEventAreaService eventAreaService;
        private readonly BLL.Interfaces.IEventService eventService;

        public UserProfileService(
            ApplicationUserManager userManager,
            IOrderService orderService,
            IEventSeatService eventSeatService,
            IEventAreaService eventAreaService,
            BLL.Interfaces.IEventService eventService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.eventSeatService = eventSeatService;
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
        }

        public UserProfileViewModel GetUserProfileViewModel(string userName)
        {
            return this.MapToUserProfileViewModel(this.userManager.FindByName(userName));
        }

        public EditUserProfileViewModel GetEditUserProfileViewModel(string userName)
        {
            return this.MapToEditUserProfileViewModel(this.userManager.FindByName(userName));
        }

        public async Task UpdateAsync(string userName, EditUserProfileViewModel userProfile)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var result = await this.userManager.UpdateAsync(this.UpdateIdentityUserModel(user, userProfile));
            if (!result.Succeeded)
            {
                throw new UpdateUserProfileException(string.Join(", ", result.Errors));
            }
        }

        public async Task ChangePasswordAsync(string userName, UserPasswordViewModel userPasswordModel)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            var result = await this.userManager.ChangePasswordAsync(user.Id, userPasswordModel.OldPassword, userPasswordModel.Password);
            if (!result.Succeeded)
            {
                throw new ChangePasswordException(string.Join(", ", result.Errors));
            }
        }

        public async Task<BalanceViewModel> GetBalanceViewModelAsync(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            return new BalanceViewModel
            {
                Balance = user.Balance,
            };
        }

        public PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName)
        {
            return this.MapToPurchaseHistoryViewModel(this.orderService.GetHistoryOrdersByName(userName).ToList());
        }

        private PurchaseHistoryViewModel MapToPurchaseHistoryViewModel(List<Order> orderList)
        {
            var purchaseHistoryVm = new PurchaseHistoryViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            var eventSeatIdArray = orderList.Select(x => x.EventSeatId).Distinct().ToArray();

            var eventSeats = this.eventSeatService.GetEventSeatsByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            var eventAreas = this.eventAreaService.GetEventAreasByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            // Event Area dictionary, Key is event seats array in cart which belong to the event area.
            var eventAreasDictionary = eventAreas.ToDictionary(x => eventSeats.Where(y => y.EventAreaId == x.Id).Select(z => z.Id), x => x);

            var events = this.eventService.GetEventsByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            // Event dictionary, Key is event seats array in cart which belong to the event.
            var eventDictionary = events.ToDictionary(x => eventSeats.Where(y => eventAreas.Any(z => z.EventId == x.Id && y.EventAreaId == z.Id)).Select(z => z.Id), x => x);

            foreach (var order in orderList)
            {
                var eventKey = eventDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));
                var eventAreaKey = eventAreasDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));

                var key = eventKey as int[] ?? eventKey.ToArray();
                var orderVm = new OrderViewModel
                {
                    DatePurchase = order.DateUtc,
                    TicketCost = eventAreasDictionary[eventAreaKey].Price,
                    EventName = eventDictionary[key].Name,
                    EventDescription = eventDictionary[key].Description,
                };

                purchaseHistoryVm.Orders.Add(orderVm);
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