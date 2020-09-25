// ****************************************************************************
// <copyright file="AdoAutofacModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using Microsoft.AspNet.Identity;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.DAL.Util
{
    public class AdoAutofacModule : Module
    {
        private readonly string connectionString;

        public AdoAutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketManagementContext>()
                .WithParameter(new TypedParameter(typeof(string), this.connectionString))
                .SingleInstance();

            builder.RegisterType<UserRepository>()
                .As(typeof(IUserStore<TicketManagementUser, int>))
                .InstancePerDependency();

            builder.RegisterType<RoleRepository>()
                .As(typeof(IRoleStore<Role, int>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType<EventRepository>()
                .As<IRepository<Event>>()
                .InstancePerDependency();
        }
    }
}
