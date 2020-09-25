// ****************************************************************************
// <copyright file="EventAlreadyPublishedException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class EventAlreadyPublishedException : Exception
    {
        public EventAlreadyPublishedException(string message)
            : base(message)
        {
        }
    }
}
