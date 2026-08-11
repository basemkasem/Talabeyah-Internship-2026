namespace EShop.Console.Entities;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public CartItem(Guid id, Guid cartId, Product product, int quantity)
    {
        Id = id;
        CartId = cartId;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
    }
}
