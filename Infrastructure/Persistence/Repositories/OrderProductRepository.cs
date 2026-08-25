using Application.Dtos.Order;
using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Persistence.Repositories;

public class OrderProductRepository(AppDbContext context) : IOrderProductRepository
{
    public void AddRange(List<OrderProductDto> orderProductsDto, Guid orderId)
    {
        List<Guid> productIds = orderProductsDto.Select(p => p.ProductId).Distinct().ToList();
        var products =
            context.Products.Where(p => productIds.Contains(p.Id));

        var orderProducts = new List<OrderProduct>();

        foreach (var orderProduct in orderProductsDto)
        {
            var productFromDb = products.SingleOrDefault(p => p.Id == orderProduct.ProductId);
            if (productFromDb is null)
                throw new Exception($"Product with id {orderProduct.ProductId} not found");
            
            productFromDb.StockQuantity -= orderProduct.Quantity;
            if (productFromDb.StockQuantity < 0)
                throw new Exception($"Product with id {orderProduct.ProductId} is out of stock");
            
            orderProducts.Add(
                new OrderProduct(
                    orderProduct.ProductId,
                    orderId,
                    orderProduct.Quantity,
                    productFromDb.Price));
            
        }

        context.OrderProducts.AddRange(orderProducts);
    }

    // public void UpdateRange(List<OrderProductDto> orderProductsDto, Guid orderId)
    // {
    //     var orderProducts =
    //         orderProductsDto.Select(opDto =>
    //             new OrderProduct(opDto.ProductId, orderId, opDto.Quantity, opDto.ItemPrice)
    //         );
    //     
    //     context.OrderProducts.UpdateRange(orderProducts);
    // }
}