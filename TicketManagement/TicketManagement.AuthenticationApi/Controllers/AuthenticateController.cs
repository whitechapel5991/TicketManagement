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
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AuthenticateController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

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

        [HttpPost]
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
    }
}
