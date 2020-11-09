// ****************************************************************************
// <copyright file="Startup.Auth.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Security.Claims;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace TicketManagement.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(4.0),
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}