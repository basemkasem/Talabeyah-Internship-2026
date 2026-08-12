using EShop.Console.Abstractions;

namespace EShop.Console.Entities;

public class Customer : ISummarizable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Order> Orders { get; private set; } = new();

    public Customer(Guid id, string name, string email, string passwordHash)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or whitespace.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password cannot be null or whitespace.");

        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public string Summarize()
    {
        return $"Customer: {Name}, Email: {Email}";
    }
}
