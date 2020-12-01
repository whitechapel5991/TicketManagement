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