using EShop.Console.Entities;

namespace EShop.Console.Services;

public class StockValidator : IStockValidator
{
    public void Validate(Cart cart)
    {
        if (cart is null)
            throw new ArgumentNullException(nameof(cart));

        foreach (var item in cart.Items)
        {
            if (item.Quantity > item.Product.StockQuantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product '{item.Product.Name}'.");
            }
        }
    }
}
