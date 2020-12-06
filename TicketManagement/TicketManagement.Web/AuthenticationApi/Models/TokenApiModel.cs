// ****************************************************************************
// <copyright file="TokenApiModel.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Newtonsoft.Json;

namespace TicketManagement.Web.AuthenticationApi.Models
{
    [JsonObject]
    public class TokenApiModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}