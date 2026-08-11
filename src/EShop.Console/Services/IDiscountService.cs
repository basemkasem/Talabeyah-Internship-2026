namespace EShop.Console.Services;

public interface IDiscountService
{
    decimal Apply(decimal subtotal);
}
