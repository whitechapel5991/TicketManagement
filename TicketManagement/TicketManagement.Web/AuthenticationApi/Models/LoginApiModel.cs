// ****************************************************************************
// <copyright file="LoginApiModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Newtonsoft.Json;

namespace TicketManagement.Web.AuthenticationApi.Models
{
    [JsonObject]
    public class LoginApiModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}