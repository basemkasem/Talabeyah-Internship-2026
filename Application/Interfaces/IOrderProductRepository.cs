using Application.Dtos;
using Application.Dtos.Order;
using Domain.Models;

namespace Application.Interfaces;

public interface IOrderProductRepository
{
    void AddRange(List<OrderProductDto> orderProductsDto, Guid orderId);
    //void UpdateRange(List<OrderProductDto> orderProductsDto, Guid orderId);
}