// ****************************************************************************
// <copyright file="VenueWithThisNameExistException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Exceptions.VenueExceptions
{
    public class VenueWithThisNameExistException : Exception
    {
        public VenueWithThisNameExistException(string message)
            : base(message)
        {
        }
    }
}
