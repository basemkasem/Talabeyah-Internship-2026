namespace Domain.Models;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private init; }
    public string PasswordHashed { get; private init; }
    
    public User(string username, string password)
    {
        Username = username;
        PasswordHashed = HashPassword(password);
    }
    public User() {}
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}