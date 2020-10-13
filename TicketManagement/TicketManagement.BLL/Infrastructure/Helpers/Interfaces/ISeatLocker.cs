// ****************************************************************************
// <copyright file="ISeatLocker.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface ISeatLocker
    {
        void UnlockSeat(int orderId);
    }
}
