// ****************************************************************************
// <copyright file="ServiceAutofacModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Reflection;
using Quartz;
using Quartz.Impl;
using TicketManagement.BLL.Infrastructure.Helpers;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.BLL.Infrastructure.Helpers.Jobs;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.BLL.Services.Identity;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models.Identity;
using Module = Autofac.Module;

namespace TicketManagement.BLL.Util
{
    public class ServiceAutofacModule : Module
    {
        private readonly string email;
        private readonly string emailPassword;
        private readonly int lockTime;

        public ServiceAutofacModule(string email, string password, int lockTime)
        {
            this.email = email;
            this.emailPassword = password;
            this.lockTime = lockTime;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VenueService>()
                .As<IVenueService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SeatService>()
                .As<ISeatService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LayoutService>()
                .As<ILayoutService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventService>()
                .As<IEventService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventSeatService>()
                .As<IEventSeatService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventAreaService>()
                .As<IEventAreaService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AreaService>()
                .As<IAreaService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderService>()
    .As<IOrderService>()
    .InstancePerLifetimeScope();

            builder.RegisterType<TicketManagementUserManager>()
                .As<UserManager<TicketManagementUser, int>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TicketManagementRoleManager>()
                .As<RoleManager<Role, int>>()
                .InstancePerLifetimeScope();

            // helpers
            builder.RegisterType<HtmlHelper>()
              .As<IHtmlHelper>()
              .InstancePerLifetimeScope();

            builder.RegisterType<GmailHelper>()
             .As<IEmailHelper>()
             .InstancePerLifetimeScope()
             .WithParameter(new NamedParameter("email", this.email))
             .WithParameter(new NamedParameter("password", this.emailPassword));

            // vaidators
            builder.RegisterType<VenueValidator>()
                .As<IVenueValidator>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SeatValidator>()
               .As<ISeatValidator>()
               .InstancePerLifetimeScope();
            builder.RegisterType<LayoutValidator>()
               .As<ILayoutValidator>()
               .InstancePerLifetimeScope();
            builder.RegisterType<EventValidator>()
               .As<IEventValidator>()
               .InstancePerLifetimeScope();
            builder.RegisterType<AreaValidator>()
               .As<IAreaValidator>()
               .InstancePerLifetimeScope();
            builder.RegisterType<OrderValidator>()
              .As<IOrderValidator>()
              .InstancePerLifetimeScope();

            // Schedule
            builder.Register(x => new StdSchedulerFactory().GetScheduler().Result).As<IScheduler>();

            // Schedule jobs
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => typeof(IJob).IsAssignableFrom(x));

            builder.RegisterType<SeatUnlockScheduler>()
                .As<ISeatUnlockScheduler>()
                .InstancePerLifetimeScope()
                .WithParameter(new NamedParameter("lockTime", this.lockTime));
        }
    }
}
