namespace UI_registrasiLogin.Interfaces
{
    public interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string Role { get; set; }
    }
}
