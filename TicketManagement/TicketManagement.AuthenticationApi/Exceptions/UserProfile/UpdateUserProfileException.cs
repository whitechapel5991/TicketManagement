// ****************************************************************************
// <copyright file="UpdateUserProfileException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.AuthenticationApi.Exceptions.UserProfile
{
    public class UpdateUserProfileException : Exception
    {
        public UpdateUserProfileException(string message)
            : base(message)
        {
        }
    }
}