namespace Domain.Models;

public class OrderStatus
{
    public Guid Id { get; private set; }
    public string Name { get; }

    public OrderStatus()
    {
        
    }
    public OrderStatus(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}