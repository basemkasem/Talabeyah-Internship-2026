using System.Runtime.CompilerServices;

namespace EShop.Console.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public List<Category> SubCategories { get; set; }
    public List<Product> Products { get; set; }
}