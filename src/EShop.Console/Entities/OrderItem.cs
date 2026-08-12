namespace EShop.Console.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid OrderId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem(Guid id, Guid orderId, Product product, int quantity, decimal price)
    {
        Id = id;
        OrderId = orderId;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        Price = price;
    }

    public decimal LineTotal => Price * Quantity;
}
