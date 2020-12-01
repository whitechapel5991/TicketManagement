using Autofac;

namespace EventWcfService.Util
{
    public class WcfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventService>();
        }
    }
}