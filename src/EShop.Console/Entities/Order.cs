using EShop.Console.Abstractions;

namespace EShop.Console.Entities;

public class Order : ISummarizable
{
    public Guid Id { get; private set; }
    public string Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderItem> OrderItems { get; private set; } = new();
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public Order(Guid id, string status, decimal totalAmount, Guid customerId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty");

        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or whitespace.");

        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer Id cannot be empty");
        Id = id;
        Status = status;
        TotalAmount = totalAmount;
        CreatedAt = DateTime.Now;
        CustomerId = customerId;
    }

    public void AddItem(Product product, int quantity)
    {
        OrderItems.Add(new OrderItem(Guid.NewGuid(), Id, product, quantity, product.Price));
        TotalAmount += product.Price * quantity;
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }

    public void SetTotalAmount(decimal totalAmount)
    {
        if (totalAmount < 0)
            throw new ArgumentException("Total amount cannot be less than zero.");

        TotalAmount = totalAmount;
    }

    public string Summarize()
    {
        return $"Order {Id}: {Status}, Total: {TotalAmount}";
    }
}
