using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicketManagement.AuthenticationApi.Models;
using TicketManagement.AuthenticationApi.Models.Extension;
using TicketManagement.BLL.Interfaces.Identity;

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
            return await Task.FromResult(this.Ok(this.userService.GetUsers().ConvertToListUserModel()));
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
                return NotFound();
            }

            return await Task.FromResult(this.Ok(user.ConvertToUserModel()));
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

            this.userService.Add(newUser.ConvertToTicketManagementUser());
            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="id">id - id user which need update.</param>
        /// <param name="editUser">editUser - new user data for updating.</param>
        /// <response code="204">Successfull</response>
        /// <response code="404">User with this id Not Found.</response>
        /// <response code="400">Wrong parameters.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel editUser)
        {
            if (editUser == null)
            {
                return await Task.FromResult(this.BadRequest());
            }

            var user = this.userService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            var bdUser = editUser.ConvertToTicketManagementUser();
            bdUser.Id = id;

            this.userService.Update(bdUser);
            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">id - id user which need delete.</param>
        /// <response code="204">Successfull</response>
        /// <response code="404">User with this id Not Found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser = this.userService.FindById(id);
            if (deleteUser == null)
            {
                return NotFound();
            }

            this.userService.Delete(deleteUser);

            return await Task.FromResult(this.NoContent());
        }
    }
}
