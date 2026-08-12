using EShop.Console.Entities;
using EShop.Console.Notifications;

namespace EShop.Console.Services;

public class OrderProcessor
{
    private readonly IStockValidator _stockValidator;
    private readonly IDiscountService _discountService;
    private readonly Notification _notification;

    public OrderProcessor(
        IStockValidator stockValidator,
        IDiscountService discountService,
        Notification notification)
    {
        _stockValidator = stockValidator;
        _discountService = discountService;
        _notification = notification;
    }

    public Order PlaceOrder(Cart cart, List<Product> products)
    {
        if (cart is null)
            throw new ArgumentNullException(nameof(cart));

        if (cart.Items.Count == 0)
            throw new InvalidOperationException("Cart is empty.");

        _stockValidator.Validate(cart, products);

        decimal subtotal = 0;
        foreach (var item in cart.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product is null)
                throw new InvalidOperationException($"Product {item.ProductId} not found.");
            
            subtotal += product.Price * item.Quantity;
        }

        var total = _discountService.Apply(subtotal);

        var order = new Order(Guid.NewGuid(), "Pending", 0m, cart.CustomerId);

        foreach (var item in cart.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);

            if (product is null)
                throw new InvalidOperationException($"Product '{item.ProductId}' not found.");

            order.AddItem(product, item.Quantity);
            product.ReduceStock(item.Quantity);
        }

        order.SetTotalAmount(total);
        
        _notification.SendConfirmation();

        return order;
    }
}
