// ****************************************************************************
// <copyright file="BllAutofacModule.cs" company="EPAM Systems">
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
using AutoMapper.Contrib.Autofac.DependencyInjection;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Util;

namespace TicketManagement.BLL.Util
{
    public class BllAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VenueService>()
                .As<IVenueService>()
                .InstancePerLifetimeScope();
            builder.AddAutoMapper(typeof(BllAutofacModule).Assembly);
        }
    }
}
