// ****************************************************************************
// <copyright file="ConfigIocContainer.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TicketManagement.UnitTests.Util
{
    internal class ConfigIocContainer
    {
        public static ContainerBuilder GetContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TestsIocModule());

            // https://stackoverflow.com/questions/8061812/using-autofac-with-moq
            return builder;
        }
    }
}
