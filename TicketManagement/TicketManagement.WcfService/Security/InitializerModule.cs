using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TicketManagement.WcfService.Security
{
    public class InitializerModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            // Sets the TLS version for every request made to an external party
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}