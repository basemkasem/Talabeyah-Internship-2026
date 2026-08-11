using EShop.Console.Entities;

namespace EShop.Console.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(Guid id);
}
