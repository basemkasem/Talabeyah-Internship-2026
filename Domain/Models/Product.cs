namespace Domain.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    
    public Product(string name, decimal price, int stockQuantity, string? description = "")
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description ?? string.Empty;
    }
}