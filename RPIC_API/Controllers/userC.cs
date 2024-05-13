using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RPIC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userC : ControllerBase
    {
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        private const string jsonFilePath = "D:\\KontruksiPerangkatLunak\\RPIC_mainProgram\\RPIC_API\\Data User\\userData.json"; 
        private readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonData, options);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonData, options);
                var user = users.Find(u => u.Username == username);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonData, options);

                if (users.Exists(u => u.Username == newUser.Username))
                {
                    return BadRequest("Username already exists. Please choose another username.");
                }

                users.Add(newUser);
                string updatedJsonData = JsonSerializer.Serialize(users, options);
                System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);

                return Ok("User has been added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{username}")]
        public IActionResult UpdateUser(string username, [FromBody] User updatedUser)
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonData, options);
                var userToUpdate = users.FirstOrDefault(u => u.Username == username);

                if (userToUpdate != null)
                {
                    userToUpdate.Username = updatedUser.Username;
                    userToUpdate.Password = updatedUser.Password;
                    userToUpdate.Role = updatedUser.Role;

                    string updatedJsonData = JsonSerializer.Serialize(users, options);
                    System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);

                    return Ok("User has been updated successfully.");
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonData, options);
                var userToDelete = users.FirstOrDefault(u => u.Username == username);

                if (userToDelete != null)
                {
                    users.Remove(userToDelete);
                    string updatedJsonData = JsonSerializer.Serialize(users, options);
                    System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);

                    return Ok("User has been deleted successfully.");
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
