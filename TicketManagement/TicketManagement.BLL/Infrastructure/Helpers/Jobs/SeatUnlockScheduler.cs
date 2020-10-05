// ****************************************************************************
// <copyright file="SeatUnlockScheduler.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Quartz;

namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    public class SeatUnlockScheduler : ISeatUnlockScheduler
    {
        private readonly int locktimeMinutes;
        private readonly IScheduler scheduler;

        public SeatUnlockScheduler(int lockTime, IScheduler scheduler)
        {
            this.locktimeMinutes = lockTime;
            this.scheduler = scheduler;
        }

        public async void Start(int eventSeatId)
        {
            await this.scheduler.Start();

            IJobDetail job = JobBuilder.Create<SeatUnlockerJob>()
                .WithIdentity($"job{eventSeatId}", "seatLockerGroup")
                .UsingJobData("eventSeatId", eventSeatId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger{eventSeatId}", "seatLockerGroup")
                .StartAt(DateBuilder.FutureDate(/*this.locktimeMinutes*/1, IntervalUnit.Second))
                .WithPriority(1)
                .ForJob(job.Key)
                .Build();

            await this.scheduler.ScheduleJob(job, trigger);
            await this.scheduler.Start();
        }

        public async void Shutdown(int eventSeatId)
        {
            await this.scheduler.Interrupt($"job{eventSeatId}");
        }
    }
}
