// ****************************************************************************
// <copyright file="AutomapperConfigTests.cs" company="EPAM Systems">
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
using AutoMapper;
using NUnit.Framework;
using TicketManagement.UnitTests.ServiceTests.Base;
using TicketManagement.UnitTests.Util;

namespace TicketManagement.UnitTests.UtilTests
{
    internal class AutomapperConfigTests : TestBase
    {
        private MapperConfiguration mapperConfiguration;

        [SetUp]
        public void Init()
        {
            var container = this.container.Build();
            this.mapperConfiguration = container.Resolve<MapperConfiguration>();
        }

        [Test]
        public void CheckMapperConfig()
        {
            this.mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
