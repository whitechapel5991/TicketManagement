using System.Web.Hosting;
using Autofac;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Services;

namespace TicketManagement.Web.Util
{
    public class WebServicesModule : Module
    {
        private readonly string eventImagePath;

        public WebServicesModule()
        {
            this.eventImagePath = HostingEnvironment.MapPath("~/Content/EventImages/");
        }

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

            builder.RegisterType<EventImageService>()
                .As(typeof(IImageService))
                .WithParameter(new NamedParameter("pathBase", this.eventImagePath))
                .InstancePerLifetimeScope();
        }
    }
}