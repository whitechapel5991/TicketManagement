// ****************************************************************************
// <copyright file="AreaWithSameDescriptionInTheLayoutExistException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.AreaExceptions
{
    public class AreaWithSameDescriptionInTheLayoutExistException : Exception
    {
        public AreaWithSameDescriptionInTheLayoutExistException(string message)
            : base(message)
        {
        }
    }
}
