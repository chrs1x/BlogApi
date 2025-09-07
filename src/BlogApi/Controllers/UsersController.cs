using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Services.UserService;

namespace BlogApi.Controllers
{
    [Route("blogapi/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all users from the db.
        /// </summary>
        /// <returns>A list of User objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() => Ok(await _userService.GetAllUsers());

        /// <summary>
        /// Finds the user with the id specified
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user with that id, or 404 if not found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Creates a new user in the db
        /// </summary>
        /// /// <param name="dto">The data transfer object containing user details.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.CreateUser(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Updates an existing user with the id specified.
        /// </summary>
        /// <param name="dto">The data transfer object containing updated user details.</param>
        /// <param name="id">The id of the user to update.</param>
        /// <returns>The updated user, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateUserDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedUser = await _userService.UpdateUser(dto, id);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

        /// <summary>
        /// Searches users by name and/or email.
        /// </summary>
        /// <param name="name">Optional name filter </param>
        /// <param name="email">Optional email filter </param>
        /// <returns>A list of users that match the search filters.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> Search(
            [FromQuery] string? name,
            [FromQuery] string? email)
        {
            var users = await _userService.GetAllUsers();

            if (!String.IsNullOrWhiteSpace(name))
            {
                users = users.Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!String.IsNullOrWhiteSpace(email))
            {
                users = users.Where(t => t.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(users.ToList());
        }

        /// <summary>
        /// Deletes a user with the id specified.
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>The deleted user, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUser(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
