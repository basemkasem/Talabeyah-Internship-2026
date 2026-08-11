namespace EShop.Console.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid? ParentCategoryId { get; private set; }
    public List<Category> SubCategories { get; private set; } = new();
    public List<Product> Products { get; private set; } = new();

    public Category(Guid id, string name, Guid? parentCategoryId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.");

        Id = id;
        Name = name;
        ParentCategoryId = parentCategoryId;
    }

    public void AddSubCategory(Category subCategory)
    {
        SubCategories.Add(subCategory);
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
}
