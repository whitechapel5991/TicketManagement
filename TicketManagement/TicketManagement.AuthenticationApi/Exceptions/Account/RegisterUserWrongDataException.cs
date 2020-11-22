// ****************************************************************************
// <copyright file="RegisterUserWrongDataException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Net;

namespace TicketManagement.AuthenticationApi.Exceptions.Account
{
    public class RegisterUserWrongDataException : BaseApiException
    {
        public RegisterUserWrongDataException(HttpStatusCode status, string message)
            : base(status, message)
        {
        }
    }
}