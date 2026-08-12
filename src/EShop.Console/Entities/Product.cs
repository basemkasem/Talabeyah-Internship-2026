using EShop.Console.Abstractions;

namespace EShop.Console.Entities;

public class Product : ISummarizable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Product(Guid id, string name, string description, decimal price, int stockQuantity, Category category)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or whitespace.");

        if (price <= 0)
            throw new ArgumentException("Product price cannot be less than zero.");

        if (stockQuantity < 0)
            throw new ArgumentException("Product stock quantity cannot be less than zero.");

        if (category.ParentCategoryId == null)
            throw new ArgumentException("You must add SubCategory not ParentCategory");

        Id = id;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        Category = category;
        CategoryId = category.Id;
    }

    public void ReduceStock(int quantity)
    {
        StockQuantity -= quantity;
    }

    public string Summarize()
    {
        return $"Product: {Name}, Price: {Price}, Stock: {StockQuantity}";
    }
}
