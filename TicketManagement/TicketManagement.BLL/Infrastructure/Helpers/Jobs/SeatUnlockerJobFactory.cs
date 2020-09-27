using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Quartz;
using Quartz.Spi;

namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    public class SeatUnlockerJobFactory : IJobFactory
    {
        private readonly IContainer container;

        public SeatUnlockerJobFactory(IContainer container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return this.container.Resolve<SeatUnlockerJob>();
        }

        public void ReturnJob(IJob job)
        {
            var disposable = (IDisposable)job;
            disposable?.Dispose();
        }
    }
}
