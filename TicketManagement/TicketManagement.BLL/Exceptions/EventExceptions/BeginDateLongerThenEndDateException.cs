// ****************************************************************************
// <copyright file="BeginDateLongerThenEndDateException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class BeginDateLongerThenEndDateException : Exception
    {
        public BeginDateLongerThenEndDateException(string message)
            : base(message)
        {
        }
    }
}
