// ****************************************************************************
// <copyright file="SeatUnlockerJob.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Threading.Tasks;
using Quartz;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class SeatUnlockerJob : IJob
    {
        private readonly IRepository<EventSeat, int> eventSeatRepository;

        public SeatUnlockerJob(IRepository<EventSeat, int> eventSeatRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
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
