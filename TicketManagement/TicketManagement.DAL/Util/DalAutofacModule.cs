// ****************************************************************************
// <copyright file="DalAutofacModule.cs" company="EPAM Systems">
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
using Autofac.Core;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Util
{
    public class DalAutofacModule : Module
    {
        private readonly string connectionString;
        private readonly string provider;

        public DalAutofacModule(string connectionString, string provider)
        {
            this.connectionString = connectionString;
            this.provider = provider;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VenueRepository>()
                .As<IRepository<Venue>>()
                .WithParameter("connectionString", this.connectionString)
                .WithParameter("provider", this.provider)
                .InstancePerLifetimeScope();
        }
    }
}
