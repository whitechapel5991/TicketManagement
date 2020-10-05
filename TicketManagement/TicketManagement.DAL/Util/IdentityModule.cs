// ****************************************************************************
// <copyright file="IdentityModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.DAL.Util
{
    internal class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleRepository>()
                .As(typeof(IRoleRepository))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserClaimRepository>()
                .As(typeof(IUserClaimRepository))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserLoginRepository>()
                .As(typeof(IUserLoginRepository))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRoleRepository>()
                .As(typeof(IUserRoleRepository))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As(typeof(IUserRepository))
                .InstancePerLifetimeScope();
        }
    }
}
