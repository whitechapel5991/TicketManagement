// ****************************************************************************
// <copyright file="ChangePasswordException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.Web.Exceptions.UserProfile
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException(string message)
            : base(message)
        {
        }
    }
}