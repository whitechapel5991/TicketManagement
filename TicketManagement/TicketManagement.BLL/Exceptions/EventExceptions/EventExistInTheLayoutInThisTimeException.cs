// ****************************************************************************
// <copyright file="EventExistInTheLayoutInThisTimeException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class EventExistInTheLayoutInThisTimeException : Exception
    {
        public EventExistInTheLayoutInThisTimeException(string message)
            : base(message)
        {
        }
    }
}
