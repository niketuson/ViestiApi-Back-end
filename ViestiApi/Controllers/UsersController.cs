using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViestiApi.Models;
using ViestiApi.Services;

namespace ViestiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            var user = await _userService.GetUserAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{username}")]
        [Authorize]
        public async Task<IActionResult> PutUser(string username, User user)
        {
            if (username != user.UserName)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUserAsync(user);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO? newUser = await _userService.CreateUserAsync(user);
            if (newUser == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetUser", new { id = newUser.UserName }, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{username}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string username)
        {
            string authName = this.User.FindFirst(ClaimTypes.Name).Value;

            bool result = await _userService.DeleteUserAsync(username);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
