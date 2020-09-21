// ****************************************************************************
// <copyright file="AdoAutofacModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Util
{
    public class AdoAutofacModule : Module
    {
        private readonly string connectionString;
        private readonly string provider;

        public AdoAutofacModule(string connectionString, string provider)
        {
            this.connectionString = connectionString;
            this.provider = provider;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketManagementContext>()
                .WithParameter(new TypedParameter(typeof(string), this.connectionString))
                .SingleInstance();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType<EventRepository>()
                .As<IRepository<Event>>()
                .InstancePerDependency();
        }
    }
}
