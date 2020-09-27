using Autofac;
using System;
using System.Configuration;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;

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

            builder.RegisterModule(new AdoAutofacModule(this.connectionString));
            builder.RegisterModule(new ServiceAutofacModule(this.email, this.emailPassword, this.lockTime));
        }
    }
}