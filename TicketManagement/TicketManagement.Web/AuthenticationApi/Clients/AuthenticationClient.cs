using RestSharp.Serialization.Json;
using TicketManagement.Web.AuthenticationApi.Base;
using TicketManagement.Web.AuthenticationApi.Models;
using TicketManagement.Web.Exceptions.Account;

namespace TicketManagement.Web.AuthenticationApi.Clients
{
    public class AuthenticationClient : ApiClient
    {
        private const string LoginUri = "api/Authenticate/login";
        private const string RegisterUri = "api/Authenticate/register";
        private const string TokenValidateUri = "api/Authenticate/Validate";

        public AuthenticationClient()
        {
        }

        public string Login(string userName, string password)
        {
            var response = this.Post(LoginUri, new LoginApiModel
            {
                UserName = userName,
                Password = password,
            });

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var deserializer = new JsonDeserializer();
                return deserializer.Deserialize<TokenApiModel>(response).Token;
            }
            else
            {
                throw new UserNameOrPasswordWrongException(Resources.TicketManagementResource.WrongCredentials);
            }
        }
    }
}