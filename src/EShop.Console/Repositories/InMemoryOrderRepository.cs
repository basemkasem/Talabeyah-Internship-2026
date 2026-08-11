using EShop.Console.Entities;

namespace EShop.Console.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public void Add(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order));

        _orders.Add(order);
    }

    public Order? GetById(Guid id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }
}
