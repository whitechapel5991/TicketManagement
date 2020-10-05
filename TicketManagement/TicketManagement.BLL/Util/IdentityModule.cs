// ****************************************************************************
// <copyright file="IdentityModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.BLL.Services.Identity;

namespace TicketManagement.BLL.Util
{
    internal class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleService>()
                .As<IRoleService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserClaimService>()
                .As<IUserClaimService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserLoginService>()
                .As<IUserLoginsService>()
                .InstancePerLifetimeScope();
        }
    }
}
