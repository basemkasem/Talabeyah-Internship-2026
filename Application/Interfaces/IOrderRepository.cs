using Application.Dtos;
using Application.Dtos.Order;
using Domain.Models;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Guid Create(Order order);
    Task<List<OrderHistoryDto>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<Order?> GetByIdAsync(Guid id);
    //Task UpdateAsync(Guid orderId, OrderDto orderDto);
    Task SaveChangesAsync();
}