// ****************************************************************************
// <copyright file="LayoutWithSameNameInTheVenueExistException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.LayoutExceptions
{
    public class LayoutWithSameNameInTheVenueExistException : Exception
    {
        public LayoutWithSameNameInTheVenueExistException(string message)
            : base(message)
        {
        }
    }
}
