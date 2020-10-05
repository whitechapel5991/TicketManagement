// ****************************************************************************
// <copyright file="ISeatUnlockScheduler.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    public interface ISeatUnlockScheduler
    {
        void Start(int eventSeatId);

        void Shutdown(int eventSeatId);
    }
}