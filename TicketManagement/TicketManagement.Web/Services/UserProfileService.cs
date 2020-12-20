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
using TicketManagement.Web.AuthenticationApi.Clients;
using TicketManagement.Web.Constants;
using TicketManagement.Web.EventAreaService;
using TicketManagement.Web.EventSeatService;
using TicketManagement.Web.EventService;
using TicketManagement.Web.Exceptions.UserProfile;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.UserProfile;
using TicketManagement.Web.OrderService;
using TicketManagement.Web.Services.Identity;
using TicketManagement.Web.WcfInfrastructure;

namespace TicketManagement.Web.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserClient userClient;

        public UserProfileService()
        {
            this.userClient = new UserClient();
        }

        public UserProfileViewModel GetUserProfileViewModel(string userName)
        {
            return this.MapToUserProfileViewModel(this.userClient.FindByName(userName));
        }

        public EditUserProfileViewModel GetEditUserProfileViewModel(string userName)
        {
            return this.MapToEditUserProfileViewModel(this.userClient.FindByName(userName));
        }

        public void Update(string userName, EditUserProfileViewModel userProfile)
        {
            var user = this.userClient.FindByName(userName);
            this.userClient.Update(this.UpdateIdentityUserModel(user, userProfile));
        }

        public void ChangePassword(string userName, UserPasswordViewModel userPasswordModel)
        {
            var user = this.userClient.FindByName(userName);
            this.userClient.ChangePassword(user.Id, userPasswordModel.OldPassword, userPasswordModel.Password);
        }

        public BalanceViewModel GetBalanceViewModelAsync(string userName)
        {
            var user = this.userClient.FindByName(userName);

            return new BalanceViewModel
            {
                Balance = user.Balance,
            };
        }

        public PurchaseHistoryViewModel GetPurchaseHistoryViewModel(string userName)
        {
            using (var client = new OrderContractClient())
            {
                client.AddClientCredentials();
                return this.MapToPurchaseHistoryViewModel(client.GetHistoryOrdersByName(userName).ToList());
            }
        }

        public void IncreaseBalance(decimal balance, string userName)
        {
            this.userClient.IncreaseBalance(userName, balance);
        }

        private PurchaseHistoryViewModel MapToPurchaseHistoryViewModel(List<Order> orderList)
        {
            var purchaseHistoryVm = new PurchaseHistoryViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            var eventSeatIdArray = orderList.Select(x => x.EventSeatId).Distinct().ToArray();

            List<EventSeat> eventSeats = null;

            List<EventArea> eventAreas = null;

            using (var client = new EventSeatContractClient())
            {
                client.AddClientCredentials();
                eventSeats = client.GetEventSeatsByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                eventAreas = client.GetEventAreasByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            // Event Area dictionary, Key is event seats array in cart which belong to the event area.
            var eventAreasDictionary = eventAreas.ToDictionary(x => eventSeats.Where(y => y.EventAreaId == x.Id).Select(z => z.Id), x => x);

            List<Event> events = null;

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                events = client.GetEventsByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            // Event dictionary, Key is event seats array in cart which belong to the event.
            var eventDictionary = events.ToDictionary(x => eventSeats.Where(y => eventAreas.Any(z => z.EventId == x.Id && y.EventAreaId == z.Id)).Select(z => z.Id).ToArray(), x => x);

            foreach (var order in orderList)
            {
                var eventKey = eventDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));
                var eventAreaKey = eventAreasDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));

                var key = eventKey ?? Array.Empty<int>().ToArray();
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