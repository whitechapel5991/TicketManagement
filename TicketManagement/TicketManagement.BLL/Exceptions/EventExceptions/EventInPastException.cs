// ****************************************************************************
// <copyright file="EventInPastException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class EventInPastException : Exception
    {
        public EventInPastException(string message)
            : base(message)
        {
        }
    }
}
