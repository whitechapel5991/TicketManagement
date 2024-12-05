// ****************************************************************************
// <copyright file="LayoutHasNotAreaException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.EventExceptions
{
    public class LayoutHasNotAreaException : Exception
    {
        public LayoutHasNotAreaException(string message)
            : base(message)
        {
        }
    }
}
