// ****************************************************************************
// <copyright file="NotEnoughMoneyException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}
