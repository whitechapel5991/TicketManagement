// ****************************************************************************
// <copyright file="WebIocModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using Autofac;
using Microsoft.AspNetCore.Identity;
using TicketManagement.AuthenticationApi.Services.Identity;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;
using IdentityRole = TicketManagement.AuthenticationApi.Services.Identity.IdentityRole;

namespace TicketManagement.AuthenticationApi.Util
{
    public class WebIocModule : Module
    {
        private readonly string connectionString;

        public WebIocModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterType<UserStore>()
                .As<IUserStore<TicketManagementUser>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoleStore>()
                .As<IRoleStore<IdentityRole>>()
                .InstancePerLifetimeScope();

            builder.RegisterModule(new WebServicesModule());
            builder.RegisterModule(new EfModule(this.connectionString));
            builder.RegisterModule(new ServiceModule());
        }
    }
}