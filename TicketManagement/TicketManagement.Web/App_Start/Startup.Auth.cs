// ****************************************************************************
// <copyright file="Startup.Auth.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace TicketManagement.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audienceId = ConfigurationManager.AppSettings["AudienceId"];
            var audienceSecret = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer, audienceSecret),
                },

                Provider = new MvcJwtAuthProvider(),
            });
        }

        private class MvcJwtAuthProvider : OAuthBearerAuthenticationProvider
        {
            public override Task RequestToken(OAuthRequestTokenContext context)
            {
                var token = context.Request.Cookies.SingleOrDefault(x => x.Key == "token").Value;

                context.Token = token;
                return base.RequestToken(context);
            }
        }
    }
}