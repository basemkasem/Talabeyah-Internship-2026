using EShop.Console.Entities;

namespace EShop.Console.Services;

public interface IStockValidator
{
    void Validate(Cart cart);
}
