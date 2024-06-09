using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace UI_registrasiLogin.Managers
{
    public class UserManager<T> where T : IUser
    {
        private List<T> users;
        private const string filePath = "D:\\New folder\\UI_registrasiLogin\\UI_registrasiLogin\\dataRegister.json";

        public UserManager()
        {
            users = LoadUsersFromFile();
        }

        public bool RegisterUser(T newUser)
        {
            if (users.Exists(u => u.Username == newUser.Username))
            {
                return false;
            }

            users.Add(newUser);
            SaveUsersToFile();
            return true;
        }

        public bool AuthenticateUser(string username, string password)
        {
            T user = users.Find(u => u.Username == username && u.Password == password);
            return user != null;
        }

        private void SaveUsersToFile()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private List<T> LoadUsersFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("Error reading JSON data. The data format might be incorrect.");
            }
            return new List<T>();
        }
    }
}
