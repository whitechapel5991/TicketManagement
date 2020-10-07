using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Services;

namespace TicketManagement.Web.Util
{
    public class WebServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>()
                .As(typeof(IAccountService))
                .InstancePerLifetimeScope();

            builder.RegisterType<CartService>()
                .As(typeof(ICartService))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventManagerEventService>()
                .As(typeof(IEventManagerEventService))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventService>()
                .As(typeof(IEventService))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserProfileService>()
                .As(typeof(IUserProfileService))
                .InstancePerLifetimeScope();
        }
    }
}