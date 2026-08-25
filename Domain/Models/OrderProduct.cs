namespace Domain.Models;

public class OrderProduct
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; }
    public Guid ProductId { get; }
    public Product Product { get; }
    
    public int Quantity { get; }
    public decimal ItemPrice { get;  }
    public decimal TotalPrice { get; }

    public OrderProduct()
    {
        
    }
    public OrderProduct(Guid productId, Guid orderId, int quantity, decimal itemPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        ItemPrice = itemPrice;
        TotalPrice = ItemPrice * Quantity;
    }
}