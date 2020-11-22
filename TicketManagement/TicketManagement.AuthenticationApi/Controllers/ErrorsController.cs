// ****************************************************************************
// <copyright file="ErrorsController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.AuthenticationApi.Exceptions;

namespace TicketManagement.AuthenticationApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var defaultErrorCode = 500;

            if (exception is BaseApiException httpException)
            {
                defaultErrorCode = (int)httpException.Status;
            }

            this.Response.StatusCode = defaultErrorCode;

            return new ErrorResponse(exception);
        }
    }
}
