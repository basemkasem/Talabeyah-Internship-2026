namespace Application.Dtos;

public record ProductDto(string Name, decimal Price, int StockQuantity, string? Description = "");