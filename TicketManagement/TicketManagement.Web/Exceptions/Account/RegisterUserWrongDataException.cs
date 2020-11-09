// ****************************************************************************
// <copyright file="RegisterUserWrongDataException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.Web.Exceptions.Account
{
    public class RegisterUserWrongDataException : Exception
    {
        public RegisterUserWrongDataException(string message)
            : base(message)
        {
        }
    }
}