using Application.Common;
using Application.Dtos.Order;

namespace Application.Services;

public interface IOrderService
{
    Task<Result<Guid>> Create(OrderDto orderDto, string? token);
    Task<Result<List<OrderHistoryDto>>> GetByCustomerId(string? token);
    //Task<Result> Update(Guid id, OrderDto orderDto, string? token);
}