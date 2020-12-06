// ****************************************************************************
// <copyright file="RegisterApiModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.AuthenticationApi.Models
{
    [JsonObject]
    public class RegisterApiModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("passwordConfirm")]
        public string PasswordConfirm { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("language")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Language Language { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}