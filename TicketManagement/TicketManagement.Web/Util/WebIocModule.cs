// ****************************************************************************
// <copyright file="WebIocModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Configuration;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Util
{
    public class WebIocModule : Module
    {
        private readonly string connectionString;
        private readonly string hangFireConnectionString;
        private readonly string email;
        private readonly string emailPassword;
        private readonly int lockTime;

        public WebIocModule(string connectionString, string hangFireConnectionString)
        {
            this.connectionString = connectionString;
            this.hangFireConnectionString = hangFireConnectionString;
            this.email = ConfigurationManager.AppSettings["Email"];
            this.emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            this.lockTime = int.Parse(ConfigurationManager.AppSettings["lockTime"]);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //builder.RegisterType<UserStore>()
            //    .As<IUserStore<IdentityUser, int>>()
            //    .As<IUserSecurityStampStore<IdentityUser, int>>()
            //    .As<IUserPasswordStore<IdentityUser, int>>()
            //    .As<IUserRoleStore<IdentityUser, int>>()
            //    .As<IUserClaimStore<IdentityUser, int>>()
            //    .As<IUserLoginStore<IdentityUser, int>>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<RoleStore>()
            //    .As<IRoleStore<IdentityRole, int>>()
            //    .As<IQueryableRoleStore<IdentityRole, int>>()
            //    .InstancePerLifetimeScope();

            builder.Register((c, p) => c.Resolve<IOwinContext>()
                .Authentication).As<IAuthenticationManager>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterModule(new WebServicesModule());
            //builder.RegisterModule(new EfModule(this.connectionString));
            //builder.RegisterModule(new ServiceModule(this.email, this.emailPassword, this.lockTime, this.hangFireConnectionString));
        }
    }
}