using Application.Dtos;
using Application.Dtos.Order;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NGuid;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public Guid Create(Order order)
    {
        context.Orders.Add(order);
        return order.Id;
    }
    // public async Task<Order> Create(OrderDto orderDto, Guid customerId)
    // {
    //     Guid orderId = GuidHelpers.CreateVersion7(); /
    //     
   
    //     
    //     orderProductRepository.AddRange(orderProductsList, orderId);
    //     
    //     var order = new Order(orderId, customerId, totalPrice);
    //     context.Add(order);
    //     return order;
    // }

    public async Task<List<OrderHistoryDto>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        var orders = context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product);

        var orderHistory =
            orders.Select(o => new OrderHistoryDto(
                o.TotalPrice,
                o.CreatedAt,
                o.OrderProducts.Select(op => new OrderProductHistoryDto(op.Product.Name, op.ItemPrice, op.Quantity))));
        return await orderHistory.ToListAsync();
    }
    
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await context.Orders.FindAsync(id);
    }

    // public async Task UpdateAsync(Guid orderId, OrderDto orderDto)
    // {
    //     var order = await GetByIdAsync(orderId);
    //     if (order is not null)
    //     {
    //         var orderProducts = orderDto.OrderProducts
    //             .Select(op => new OrderProduct(op.ProductId, orderId, op.Quantity,op.)).ToList();
    //     
    //         orderProductRepository.UpdateRange(orderDto.OrderProducts, orderId);
    //         
    //         order.Update(orderProducts.Sum(op => op.Quantity * op.ItemPrice));
    //     }
    // }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}