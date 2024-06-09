using MyApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MyApi.Services
{
    public class UserService
    {
        private const string filePath = "dataRegister.json";
        private List<User> users;

        public UserService()
        {
            users = LoadUsersFromFile();
        }

        public List<User> GetUsers() => users;

        public User GetUser(string username) => users.FirstOrDefault(u => u.Username == username);

        public bool RegisterUser(User newUser)
        {
            if (users.Any(u => u.Username == newUser.Username))
            {
                return false;
            }

            users.Add(newUser);
            SaveUsersToFile();
            return true;
        }

        public User AuthenticateUser(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool UpdateUser(User updatedUser)
        {
            var existingUser = users.FirstOrDefault(u => u.Username == updatedUser.Username);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Password = updatedUser.Password;
            existingUser.Role = updatedUser.Role;
            SaveUsersToFile();
            return true;
        }

        public bool DeleteUser(string username)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }

            users.Remove(user);
            SaveUsersToFile();
            return true;
        }

        private void SaveUsersToFile()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private List<User> LoadUsersFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            return new List<User>();
        }
    }
}
