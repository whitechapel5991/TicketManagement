// ****************************************************************************
// <copyright file="SeatCurrentlySoldOrBlocked.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class SeatCurrentlySoldOrBlocked : Exception
    {
        public SeatCurrentlySoldOrBlocked(string message)
            : base(message)
        {
        }
    }
}
