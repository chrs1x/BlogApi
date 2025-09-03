using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Services.UserService;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() => Ok(await _userService.GetAllUsers());

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.CreateUser(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateUserDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedUser = await _userService.UpdateUser(dto, id);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUser(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
