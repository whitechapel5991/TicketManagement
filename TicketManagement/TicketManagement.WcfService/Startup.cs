using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Hangfire;

[assembly: OwinStartup(typeof(TicketManagement.WcfService.Startup))]

namespace TicketManagement.WcfService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
