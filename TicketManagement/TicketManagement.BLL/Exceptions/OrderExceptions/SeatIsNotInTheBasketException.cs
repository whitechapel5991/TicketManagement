// ****************************************************************************
// <copyright file="SeatIsNotInTheBasketException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class SeatIsNotInTheBasketException : Exception
    {
        public SeatIsNotInTheBasketException(string message)
            : base(message)
        {
        }
    }
}
