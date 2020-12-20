using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;

namespace TicketManagement.Web.WcfInfrastructure
{
    public static class WcfClientExtensions
    {
        public static void AddClientCredentials<TChannel>(this ClientBase<TChannel> client)
            where TChannel : class
        {
            if (client.ClientCredentials != null)
            {
                client.ClientCredentials.UserName.UserName = CredentialsContainer.UserName;
                client.ClientCredentials.UserName.Password = CredentialsContainer.Password;
            }
        }
    }
}