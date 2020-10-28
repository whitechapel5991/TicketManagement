// ****************************************************************************
// <copyright file="EfModule.cs" company="EPAM Systems">
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
    public class EfModule : Module
    {
        private readonly string connectionString;

        public EfModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GenerateDbContext>()
                .As<IGenerateDbContext>()
                .WithParameter(new TypedParameter(typeof(string), this.connectionString))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventRepository>()
                .As<IRepository<Event>>()
                .InstancePerLifetimeScope();

            builder.RegisterModule(new IdentityModule());
        }
    }
}
