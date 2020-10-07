using Autofac;
using System;
using System.Configuration;
using System.Web;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Services;
using TicketManagement.Web.Services.Identity;

namespace TicketManagement.Web.Util
{
    public class WebIocModule : Module
    {
        private readonly string connectionString;
        private readonly string email;
        private readonly string emailPassword;
        private readonly int lockTime;

        public WebIocModule(string connectionString)
        {
            this.connectionString = connectionString;
            this.email = ConfigurationManager.AppSettings["Email"];
            this.emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            this.lockTime = int.Parse(ConfigurationManager.AppSettings["lockTime"]);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UserStore>()
                .As<IUserStore<IdentityUser, int>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleStore>()
                .As<IRoleStore<IdentityRole, int>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleStore>()
                .As<IRoleStore<IdentityRole, int>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager<IdentityUser, int>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register<IAuthenticationManager>(context => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();
            
            builder.RegisterModule(new WebServicesModule());
            builder.RegisterModule(new EfAutofacModule(this.connectionString));
            builder.RegisterModule(new ServiceAutofacModule(this.email, this.emailPassword, this.lockTime));
        }
    }
}