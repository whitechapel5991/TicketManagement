// ****************************************************************************
// <copyright file="ServiceModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using Autofac;
using Hangfire;
using Hangfire.SqlServer;
using TicketManagement.BLL.Infrastructure.Helpers;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.Services;
using TicketManagement.BLL.ServiceValidators;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using Module = Autofac.Module;

namespace TicketManagement.BLL.Util
{
    public class ServiceModule : Module
    {
        private readonly string email;
        private readonly string emailPassword;
        private readonly int lockTime;
        private readonly string hangFireConnectionString;

        public ServiceModule()
        {
            this.email = default;
            this.emailPassword = default;
            this.lockTime = default;
            this.hangFireConnectionString = default;
        }

        public ServiceModule(string email, string password, int lockTime, string hangFireConnectionString)
        {
            this.email = email;
            this.emailPassword = password;
            this.lockTime = lockTime;
            this.hangFireConnectionString = hangFireConnectionString;
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

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope()
                .WithParameter(new NamedParameter("seatLockTimeMinutes", this.lockTime));

            // Helpers
            builder.RegisterType<GmailHelper>()
             .As<IEmailHelper>()
             .InstancePerLifetimeScope()
             .WithParameter(new NamedParameter("email", this.email))
             .WithParameter(new NamedParameter("password", this.emailPassword));

            builder.RegisterType<DataTimeHelper>()
                .As<IDataTimeHelper>()
                .InstancePerLifetimeScope();

            // Validators
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

            // HangFire
            if (this.hangFireConnectionString != default)
            {
                this.HangFireConfig(this.hangFireConnectionString);
                builder.RegisterType<BackgroundJobClient>().As<IBackgroundJobClient>().InstancePerLifetimeScope();
                builder.Register(c => JobStorage.Current).As<JobStorage>().InstancePerLifetimeScope();
                builder.RegisterType<SeatLocker>()
                    .As<ISeatLocker>()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterModule(new IdentityModule());
        }

        private void HangFireConfig(string connectionString)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(1),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(1),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                });
        }
    }
}
