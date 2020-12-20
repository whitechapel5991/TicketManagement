// ****************************************************************************
// <copyright file="UserController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicketManagement.AuthenticationApi.Models;
using TicketManagement.AuthenticationApi.Models.Extension;
using TicketManagement.AuthenticationApi.Services;

namespace TicketManagement.AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [SwaggerResponse((int)System.Net.HttpStatusCode.Unauthorized)]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <response code="200">Returns all users.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(this.Ok(this.userService.GetUsers()));
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">id - User id.</param>
        /// <response code="200">Returns user.</response>
        /// <response code="404">User with this id not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = this.userService.FindById(id);
            if (user == null)
            {
                return this.NotFound();
            }

            return await Task.FromResult(this.Ok(user));
        }

        /// <summary>
        /// Get user by userName.
        /// </summary>
        /// <param name="userName">userName - User Name.</param>
        /// <response code="200">Returns user.</response>
        /// <response code="404">User with this userName not found.</response>
        [HttpGet("find/{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = this.userService.FindByName(userName);
            if (user == null)
            {
                return this.NotFound();
            }

            return await Task.FromResult(this.Ok(user));
        }

        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="newUser">newUser - new User which need save.</param>
        /// <response code="204">Success.</response>
        /// <response code="400">Wrong parameters.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel newUser)
        {
            if (newUser == null)
            {
                return await Task.FromResult(this.BadRequest());
            }

            var result = this.userService.Add(newUser);
            if (!result.Succeeded)
            {
                return await Task.FromResult(this.BadRequest(string.Join(", ", result.Errors)));
            }

            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="id">id - id user which need update.</param>
        /// <param name="editUser">editUser - new user data for updating.</param>
        /// <response code="204">Successfully.</response>
        /// <response code="404">User with this id Not Found.</response>
        /// <response code="400">Wrong parameters.</response>
        [HttpPut()]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel editUser)
        {
            if (editUser == null)
            {
                return await Task.FromResult(this.BadRequest());
            }

            var user = this.userService.FindById(id);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = this.userService.Update(id, editUser);
            if (!result.Succeeded)
            {
                return await Task.FromResult(this.BadRequest(string.Join(", ", result.Errors)));
            }

            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="userId">userId - id user which need change password.</param>
        /// <param name="oldPassword">oldPassword - old user password.</param>
        /// <param name="newPassword">newPassword - new user password.</param>
        /// <response code="204">Successfully.</response>
        /// <response code="404">User with this id Not Found.</response>
        /// <response code="400">Wrong parameters.</response>
        [HttpPut("{userId}/Password/{oldPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null)
            {
                return await Task.FromResult(this.BadRequest());
            }

            var user = this.userService.FindById(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await this.userService.ChangePassword(userId, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                return await Task.FromResult(this.BadRequest(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description))));
            }

            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Increase user balance.
        /// </summary>
        /// <param name="userName">userName - Username which balance need increase.</param>
        /// <param name="balance">balance - additional balance.</param>
        /// <response code="204">Successfully.</response>
        /// <response code="400">Wrong parameters.</response>
        [HttpPut("{userName}/{balance}")]
        public async Task<IActionResult> IncreaseBalance(string userName, decimal balance)
        {
            if (userName == null)
            {
                return await Task.FromResult(this.BadRequest());
            }

            this.userService.IncreaseBalance(balance, userName);
            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">id - id user which need delete.</param>
        /// <response code="204">Successfully.</response>
        /// <response code="404">User with this id Not Found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser = this.userService.FindById(id);
            if (deleteUser == null)
            {
                return this.NotFound();
            }

            this.userService.Delete(id);
            return await Task.FromResult(this.NoContent());
        }
    }
}
