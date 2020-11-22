// ****************************************************************************
// <copyright file="BaseApiException.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Net;

namespace TicketManagement.AuthenticationApi.Exceptions
{
    public class BaseApiException : Exception
    {
        public BaseApiException(HttpStatusCode status, string message)
            : base(message)
        {
            this.Status = status;
        }

        public HttpStatusCode Status { get; private set; }
    }
}
