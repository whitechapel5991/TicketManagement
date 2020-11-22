// ****************************************************************************
// <copyright file="WebServicesModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using TicketManagement.AuthenticationApi.Services;

namespace TicketManagement.AuthenticationApi.Util
{
    public class WebServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>()
                .As(typeof(IAccountService))
                .InstancePerLifetimeScope();
        }
    }
}