using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using RestSharp.Serialization.Json;
using TicketManagement.Web.AuthenticationApi.Base;
using TicketManagement.Web.AuthenticationApi.Models;
using TicketManagement.Web.Exceptions.Account;
using TicketManagement.Web.Exceptions.UserProfile;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.AuthenticationApi.Clients
{
    public class UserClient : ApiClient
    {
        private const string IncreaseBalanceUri = "api/User/{0}/{1}";
        private const string FindByNameUri = "api/User/find/{0}/";
        private const string UpdateUserUri = "api/User/{0}/";
        private const string ChangeUserPasswordUri = "api/User/{0}/Password/{1}/{2}";

        public void IncreaseBalance(string userName, decimal balance)
        {
            this.Put(string.Format(IncreaseBalanceUri, userName, balance));
        }

        public IdentityUser FindByName(string userName)
        {
            return this.Get<IdentityUser>(string.Format(FindByNameUri, userName));
        }

        public void Update(IdentityUser user)
        {
            var response = this.Put(string.Format(UpdateUserUri, user.Id));

            if (!response.IsSuccessful)
            {
                throw new UpdateUserProfileException(response.ErrorMessage);
            }
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var response = this.Put(string.Format(ChangeUserPasswordUri, userId, oldPassword, newPassword));

            if (!response.IsSuccessful)
            {
                throw new ChangePasswordException(response.ErrorMessage);
            }
        }
    }
}