using Application.Common;
using Application.Dtos;
using Application.Dtos.Order;
using Domain.Models;

namespace Application.Errors;

public static class OrderErrors
{
    public static Error OrderNotFound(Guid id) => 
        Error.NotFound(nameof(Order), $"Order with '{id}' was not found.");
    
    public static Error ProductNotFound(Guid id) => 
        Error.NotFound(nameof(Product), $"Product with '{id}' was not found."); 
    
    public static Error StockQuantityIsNegativeOrZero() =>
        Error.Validation(nameof(OrderProductDto.Quantity), "Product quantity can't be negative or zero.");
    
    public static Error QuantityExceededStock() =>
        Error.Validation(nameof(OrderProductDto.Quantity), "The requested quantity exceeds the available stock.");
    
    public static Error MissingAuthenticationToken() => 
        Error.NotAuthorized("Authentication.MissingToken", "Missing authentication token.");
    
    public static Error InvalidToken() => 
        Error.NotAuthorized("Authentication.InvalidToken", "Token format is incorrect.");
}