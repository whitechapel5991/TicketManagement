// ****************************************************************************
// <copyright file="SomeAreaHasNotPriceException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class SomeAreaHasNotPriceException : Exception
    {
        public SomeAreaHasNotPriceException(string message)
            : base(message)
        {
        }
    }
}
