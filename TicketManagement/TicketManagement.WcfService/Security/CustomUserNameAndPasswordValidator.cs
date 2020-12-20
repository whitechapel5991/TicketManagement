using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IdentityModel.Selectors;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System.IdentityModel.Tokens.Jwt;

namespace TicketManagement.WcfService.Security
{
    public class CustomUserNameAndPasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (this.GetClientIdentity(userName, password))
                return;

            throw new FaultException("Invalid Username and/or Password");
        }

        private bool GetClientIdentity(string userName, string password)
        {
            RestClient httpClient = new RestClient(ConfigurationManager.AppSettings["AuthApiAddress"]);
            var request = new RestRequest("/api/Authenticate/login", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new { userName, password });
            var response = httpClient.Execute(request);

            //var token = Encoding.UTF8.GetString(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var handler = new JwtSecurityTokenHandler();
                var deserializer = new JsonDeserializer();
                var token = deserializer.Deserialize<TokenApiModel>(response).Token;
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                return true;
            }

            return false;

            //using (var client = new HttpClient())
            //{
            //    //Api Base address
            //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AuthApiAddress"]);
            //    //client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //client.DefaultRequestHeaders.AcceptLanguage.Clear();
            //    //StringWithQualityHeaderValue lang = new StringWithQualityHeaderValue(Language.EN);
            //    //client.DefaultRequestHeaders.AcceptLanguage.Add(lang);
            //    //client.DefaultRequestHeaders.Add("Authorization", TokenContainer.Token);
            //    var credential = new List<KeyValuePair<string, string>>();
            //    credential.Add(new KeyValuePair<string, string>("userName", userName));
            //    credential.Add(new KeyValuePair<string, string>("password", password));
            //    HttpResponseMessage response =
            //        client.PostAsync("/api/Authenticate/login", new FormUrlEncodedContent(credential)).Result;

            //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        return true;
            //    }

            //    return false;
            //}
        }
    }
}