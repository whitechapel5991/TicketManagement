// ****************************************************************************
// <copyright file="UserNameOrPasswordWrongException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.Web.Exceptions.Account
{
    public class UserNameOrPasswordWrongException : Exception
    {
        public UserNameOrPasswordWrongException(string message)
            : base(message)
        {
        }
    }
}