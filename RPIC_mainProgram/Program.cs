using System;

class Program
{
    static void Main(string[] args)
    {
        userManager<User> userMgr = new userManager<User>();

        Console.WriteLine("Registrasi Pengguna Baru:");
        userMgr.RegisterUser();

        Console.WriteLine("\nAutentikasi Pengguna:");
        userMgr.AuthenticateUser();
    }
}
