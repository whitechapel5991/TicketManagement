using Autofac;
using System;
using TicketManagement.BLL.Util;
using TicketManagement.DAL.Util;

namespace TicketManagement.Web.Util
{
    public class WebIocModule : Module
    {
        private readonly string connectionString;

        public WebIocModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterModule(new AdoAutofacModule(this.connectionString));
            builder.RegisterModule(new ServiceAutofacModule());
        }
    }
}