using System.Configuration;
using RestSharp;

namespace TicketManagement.Web.AuthenticationApi.Base
{
    public class ApiClient
    {
        private readonly RestClient httpClient;

        protected ApiClient()
        {
            this.httpClient = new RestClient(ConfigurationManager.AppSettings["AuthenticateApiUrl"]);
        }

        public T Get<T>(string route)
        {
            var request = new RestRequest(route, Method.GET);
            return this.httpClient.Execute<T>(request).Data;
        }

        protected IRestResponse Post<T>(string route, T body)
        {
            var request = new RestRequest(route, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            return this.httpClient.Execute(request);
        }
    }
}