using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TicketManagement.Web.Startup))]
namespace TicketManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAutofacMiddleware(IocContainerConfig.Container);
            app.UseAutofacMvc();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            this.ConfigureAuth(app);
        }
    }
}