// ****************************************************************************
// <copyright file="EntityDoesNotExistException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.Base
{
    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(string message)
            : base(message)
        {
        }
    }
}
