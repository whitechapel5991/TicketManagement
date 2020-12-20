// ****************************************************************************
// <copyright file="ApiClient.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Configuration;
using RestSharp;
using TicketManagement.Web.WcfInfrastructure;

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
            this.AddAuthorizationTokenHeader(request);
            return this.httpClient.Execute<T>(request).Data;
        }

        protected IRestResponse Post<T>(string route, T body)
        {
            var request = new RestRequest(route, Method.POST) { RequestFormat = DataFormat.Json };
            this.AddAuthorizationTokenHeader(request);
            request.AddJsonBody(body);
            return this.httpClient.Execute(request);
        }

        protected IRestResponse Put(string route)
        {
            var request = new RestRequest(route, Method.PUT) { RequestFormat = DataFormat.Json };
            this.AddAuthorizationTokenHeader(request);
            return this.httpClient.Execute(request);
        }

        protected IRestResponse Put<T>(string route, T body)
        {
            var request = new RestRequest(route, Method.PUT) { RequestFormat = DataFormat.Json };
            this.AddAuthorizationTokenHeader(request);
            request.AddJsonBody(body);
            return this.httpClient.Execute(request);
        }

        private void AddAuthorizationTokenHeader(IRestRequest request)
        {
            if (CredentialsContainer.Token != default)
            {
                request.AddHeader("Authorization", $"Bearer {CredentialsContainer.Token}");
            }
        }
    }
}