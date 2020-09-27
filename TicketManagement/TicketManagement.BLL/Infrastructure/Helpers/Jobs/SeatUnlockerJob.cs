using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class SeatUnlockerJob : IJob
    {
        private IRepository<EventSeat> eventSeatRepository;

        public SeatUnlockerJob(IRepository<EventSeat> eventSeatRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.MergedJobDataMap;

            var eventSeat = this.eventSeatRepository.GetById(dataMap.GetIntValue("eventSeatId"));
            if (eventSeat.State == EventSeatState.InBasket)
            {
                eventSeat.State = EventSeatState.Free;
                this.eventSeatRepository.Update(eventSeat);
            }

            Console.Write("COMPLETE!");
            return Task.CompletedTask;
        }
    }
}
