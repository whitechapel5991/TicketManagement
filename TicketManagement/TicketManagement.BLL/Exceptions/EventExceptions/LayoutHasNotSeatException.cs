// ****************************************************************************
// <copyright file="LayoutHasNotSeatException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class LayoutHasNotSeatException : Exception
    {
        public LayoutHasNotSeatException(string message)
            : base(message)
        {
        }
    }
}
