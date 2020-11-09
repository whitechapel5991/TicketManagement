// ****************************************************************************
// <copyright file="SeatCurrentlySoldOrBlockedException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class SeatCurrentlySoldOrBlockedException : Exception
    {
        public SeatCurrentlySoldOrBlockedException(string message)
            : base(message)
        {
        }
    }
}
