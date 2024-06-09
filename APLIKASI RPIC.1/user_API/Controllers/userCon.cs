using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services;
using System.Collections.Generic;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController()
        {
            userService = new UserService();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (userService.RegisterUser(newUser))
            {
                return Ok("User has been registered.");
            }
            return BadRequest("Username already exists.");
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User loginUser)
        {
            var user = userService.AuthenticateUser(loginUser.Username, loginUser.Password);
            if (user != null)
            {
                return Ok(new { message = "Login successful.", role = user.Role });
            }
            return Unauthorized("Invalid username or password.");
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(userService.GetUsers());
        }

        [HttpGet("{username}")]
        public ActionResult<User> GetUser(string username)
        {
            var user = userService.GetUser(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User updatedUser)
        {
            if (userService.UpdateUser(updatedUser))
            {
                return Ok("User has been updated.");
            }
            return NotFound("User not found.");
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            if (userService.DeleteUser(username))
            {
                return Ok("User has been deleted.");
            }
            return NotFound("User not found.");
        }
    }
}
