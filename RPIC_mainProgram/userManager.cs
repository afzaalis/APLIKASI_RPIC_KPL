using System;
using System.Collections.Generic;

public class userManager<T> where T : IUser
{
    private List<T> users;

    public userManager()
    {
        users = new List<T>();
    }

    public bool RegisterUser()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        if (users.Exists(u => u.Username == username))
        {
            Console.WriteLine("Username already exists. Please choose another username.");
            return false;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Console.Write("Enter role: ");
        string role = Console.ReadLine();

        T newUser = (T)Activator.CreateInstance(typeof(T));
        newUser.Username = username;
        newUser.Password = password;
        newUser.Role = role;

        users.Add(newUser);

        Console.WriteLine("User has been registered.");
        return true;
    }

    public bool AuthenticateUser()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        T user = users.Find(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            Console.WriteLine("Login successful.");
            Console.WriteLine("User role: " + user.Role);
            return true;
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
            return false;
        }
    }
}

public interface IUser
{
    string Username { get; set; }
    string Password { get; set; }
    string Role { get; set; }
}