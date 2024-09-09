using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        // Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Create a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user, "Password123!");
            if (!result.Succeeded) return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Update an existing user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] IdentityUser user)
        {
            if (id != user.Id) return BadRequest();

            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        // Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
