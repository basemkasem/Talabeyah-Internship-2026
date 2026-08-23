using Application.Common;
using Application.Dtos;
using Domain.Models;

namespace Application.Errors;

public static class ProductErrors
{
    public static Error ProductNotFound(Guid id) => 
        Error.NotFound(nameof(Product), $"Product with '{id}' was not found.");

    public static Error NameExceedMaxLength() =>
        Error.Validation(nameof(ProductDto.Name), "Product name length can't be more than 150 characters");

    public static Error NameTooShort() =>
        Error.Validation(nameof(ProductDto.Name), "Product name length should be 3 characters at least.");
    
    public static Error PriceIsNegative() =>
        Error.Validation(nameof(ProductDto.Price), "Product price can't be negative.");
    
    public static Error StockQuantityIsNegative() =>
        Error.Validation(nameof(ProductDto.StockQuantity), "Product quantity can't be negative.");
    
    public static Error DescriptionExceedMaxLength() =>
        Error.Validation(nameof(ProductDto.Description), "Product description length can't be more than 150 characters");

    public static Error DescriptionTooShort() =>
        Error.Validation(nameof(ProductDto.Description), "Product description length should be 3 characters at least.");
}