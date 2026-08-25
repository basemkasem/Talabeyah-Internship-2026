using System.IdentityModel.Tokens.Jwt;
using Application.Common;
using Application.Dtos.Order;
using Application.Errors;
using Application.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using NGuid;

namespace Application.Services;

public class OrderService(IRepositoryManager repositoryManager) : IOrderService
{
    public async Task<Result<Guid>> Create(OrderDto orderDto, string? token)
    {
        var validateCustomerResult = ValidateCustomer(token);
        if (!validateCustomerResult.IsSuccess)
        {
            return Result.Failure<Guid>(validateCustomerResult.Error);
        }

        var orderProductsList = new List<OrderProductDto>();
        var totalPrice = 0m;

        foreach (var orderProduct in orderDto.OrderProducts)
        {
            var productFromDb = await repositoryManager.Product.GetById(orderProduct.ProductId);
            if (productFromDb is null)
                return OrderErrors.ProductNotFound(orderProduct.ProductId);

            var validatingOrderDtoResult = ValidateOrderProductDto<Guid>(orderProduct, productFromDb);
            if (!validatingOrderDtoResult?.IsSuccess ?? false)
            {
                return validatingOrderDtoResult;
            }

            orderProductsList.Add(new OrderProductDto(orderProduct.ProductId, orderProduct.Quantity));
            totalPrice += orderProduct.Quantity * productFromDb.Price;
        }

        Guid orderId = GuidHelpers.CreateVersion7();
        var customerId = validateCustomerResult.Data;

        repositoryManager.Order.Create(new Order(orderId, customerId, totalPrice));

        repositoryManager.OrderProduct.AddRange(orderProductsList, orderId);

        await repositoryManager.SaveAsync();
        return orderId;
    }

    public async Task<Result<List<OrderHistoryDto>>> GetByCustomerId(string? token)
    {
        var validateCustomerResult = ValidateCustomer(token);
        if (!validateCustomerResult.IsSuccess)
        {
            return Result.Failure<List<OrderHistoryDto>>(validateCustomerResult.Error);
        }

        var customerId = validateCustomerResult.Data;
        return await repositoryManager.Order.GetOrdersByCustomerIdAsync(customerId);
    }

    // public async Task<Result> Update(Guid id, OrderDto orderDto, string? token)
    // {
    //     var orderFromDb = await orderRepository.GetByIdAsync(id);
    //     if (orderFromDb is null)
    //         return OrderErrors.OrderNotFound(id);
    //     
    //     var validationResult = await ValidateOrderDto<Guid>(orderDto);
    //     if (!validationResult?.IsSuccess ?? false)
    //     {
    //         return validationResult;
    //     }
    //     
    //     await orderRepository.UpdateAsync(id, orderDto);
    //     return Result.Success();
    // }

    private Result<Guid> ValidateCustomer(string? token)
    {
        if (token.IsNullOrEmpty())
        {
            return OrderErrors.MissingAuthenticationToken();
        }

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        if (!Guid.TryParse(jwt.Claims.Single(x => x.Type == "nameid").Value, out var customerId))
        {
            return OrderErrors.InvalidToken();
        }

        return customerId;
    }

    private Result<T>? ValidateOrderProductDto<T>(OrderProductDto productDto, Product productFromDb)
    {
        if (productDto.Quantity <= 0)
        {
            return OrderErrors.StockQuantityIsNegativeOrZero();
        }

        if (productFromDb.StockQuantity < productDto.Quantity)
        {
            return OrderErrors.QuantityExceededStock();
        }

        return null;
    }

    // private Result<T>? ValidateCustomerId<T>(string? token)
    // {
    //     if (token is null) 
    //         OrderErrors.MissingAuthenticationToken();
    //
    //     var handler = new JwtSecurityTokenHandler();
    //     var jwt = handler.ReadJwtToken(token);
    //     var customerId = jwt.Id;
    //
    // }
}