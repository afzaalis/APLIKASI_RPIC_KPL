using System;

namespace UI_registrasiLogin.Models
{
    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

public interface IUser
{
    string Username { get; set; }
    string Password { get; set; }
    string Role { get; set; }
}
