// ****************************************************************************
// <copyright file="ConfigIocContainer.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using Hangfire;

namespace TicketManagement.IntegrationTests.Util
{
    internal static class ConfigIocContainer
    {
        public static IContainer GetIocContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TestsIocModule());

            var container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            return container;
        }
    }
}
