// ****************************************************************************
// <copyright file="LayoutHasSoldSeatAndCouldNotBeChangeException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class LayoutHasSoldSeatAndCouldNotBeChangeException : Exception
    {
        public LayoutHasSoldSeatAndCouldNotBeChangeException(string message)
            : base(message)
        {
        }
    }
}
