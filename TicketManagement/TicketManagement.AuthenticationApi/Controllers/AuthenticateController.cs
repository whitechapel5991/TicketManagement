// ****************************************************************************
// <copyright file="AuthenticateController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.AuthenticationApi.Infrastructure;
using TicketManagement.AuthenticationApi.Models;
using TicketManagement.AuthenticationApi.Services;

namespace TicketManagement.AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AuthenticateController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Login into the system.
        /// </summary>
        /// <param name="model">model - Username and password.</param>
        /// <response code="200">Returns the newly access token.</response>
        /// <response code="400">Wrong parameters.</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var token = await this.accountService.SignInAsync(model.UserName, model.Password);
            return this.Ok(new
            {
                token,
            });
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="model">model - new RegisterModel.</param>
        /// <returns>User created successfully!.</returns>
        /// <response code="200">Returns new Response { Status = "Success", Message = "User created successfully!" }.</response>
        /// <response code="400">Wrong parameters.</response>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.accountService.RegisterUserAsync(model);
            return this.Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary>
        /// Validate token.
        /// </summary>
        /// <param name="token">token - access token.</param>
        /// <response code="200">Returns new Response { Status = "Success", Message = "Valid Token." }.</response>
        /// <response code="400">Returns new Response { Status = "Invalid", Message = "Invalid Token." }.</response>
        [Route("Validate")]
        [HttpGet]
        public IActionResult Validate(string token)
        {
            var username = this.accountService.ValidateToken(token);

            if (username == default)
            {
                return this.BadRequest(new Response { Status = "Invalid", Message = "Invalid Token." });
            }

            return this.Ok(new Response { Status = "Success", Message = "Valid Token." });
        }
    }
}
