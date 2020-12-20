using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TicketManagement.WcfService.Security
{
    [JsonObject]
    public class TokenApiModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}