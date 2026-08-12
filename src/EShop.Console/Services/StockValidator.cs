using EShop.Console.Entities;

namespace EShop.Console.Services;

public class StockValidator : IStockValidator
{
    public void Validate(Cart cart, List<Product> products)
    {
        if (cart is null)
            throw new ArgumentNullException(nameof(cart));

        if (products is null)
            throw new ArgumentNullException(nameof(products));

        foreach (var item in cart.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);

            if (product is null)
                throw new InvalidOperationException($"Product '{item.ProductId}' not found.");

            if (item.Quantity > product.StockQuantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product '{product.Name}'.");
            }
        }
    }
}
