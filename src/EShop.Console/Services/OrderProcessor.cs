using EShop.Console.Entities;
using EShop.Console.Notifications;
using EShop.Console.Repositories;

namespace EShop.Console.Services;

public class OrderProcessor
{
    private readonly IStockValidator _stockValidator;
    private readonly IDiscountService _discountService;
    private readonly IOrderRepository _orderRepository;
    private readonly Notification _notification;

    public OrderProcessor(
        IStockValidator stockValidator,
        IDiscountService discountService,
        IOrderRepository orderRepository,
        Notification notification)
    {
        _stockValidator = stockValidator;
        _discountService = discountService;
        _orderRepository = orderRepository;
        _notification = notification;
    }

    public Order PlaceOrder(Customer customer, Cart cart)
    {
        if (customer is null)
            throw new ArgumentNullException(nameof(customer));
        
        if (cart.Items.Count == 0)
            throw new InvalidOperationException("Cart is empty.");

        _stockValidator.Validate(cart);

        decimal subtotal = 0;
        foreach (var item in cart.Items)
        {
            subtotal += item.Product.Price * item.Quantity;
        }

        var total = _discountService.Apply(subtotal);

        var order = new Order(Guid.NewGuid(), "Pending", 0m, customer);

        foreach (var item in cart.Items)
        {
            order.AddItem(item.Product, item.Quantity);
            item.Product.ReduceStock(item.Quantity);
        }

        order.SetTotalAmount(total);

        _orderRepository.Add(order);
        customer.AddOrder(order);
        _notification.SendConfirmation();

        return order;
    }
}
