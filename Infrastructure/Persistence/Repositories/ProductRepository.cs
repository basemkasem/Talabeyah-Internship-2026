using Application.Dtos;
using Application.Interfaces;
using Application.Shared;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public Product Add(ProductDto userDto)
    {
        var product = new Product(userDto.Name, userDto.Price, userDto.StockQuantity, userDto.Description);
        context.Products.Add(product);
        return product;
    }

    public async Task Update(Guid id, ProductDto userDto)
    {
        var product = await GetById(id);
        if (product is not null)
        {
            product.Name = userDto.Name;
            product.Price = userDto.Price;
            product.StockQuantity = userDto.StockQuantity;
            product.Description = userDto.Description ?? string.Empty;
        }
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetListPaginated(PaginationParams paginationParams)
    {
        return await context.Products.Skip(paginationParams.PageNumber - 1).Take(paginationParams.PageSize).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}