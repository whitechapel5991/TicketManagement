// ****************************************************************************
// <copyright file="ErrorResponse.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.AuthenticationApi.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(Exception ex)
        {
            this.Type = ex.GetType().Name;
            this.Message = ex.Message;
            this.StackTrace = ex.ToString();
        }

        public string Type { get; set; }

        public string Message { get; private set; }

        public string StackTrace { get; set; }
    }
}
