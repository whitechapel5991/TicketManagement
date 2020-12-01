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