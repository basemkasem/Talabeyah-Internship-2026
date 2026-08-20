using Application.Dtos;
using Application.Shared;
using Domain.Models;

namespace Application.Interfaces;

public interface IProductRepository
{
    Product Add(ProductDto userDto);
    Task Update(Guid id, ProductDto userDto);
    Task<Product?> GetById(Guid id);
    Task<List<Product>> GetListPaginated(PaginationParams paginationParams);
    Task SaveChangesAsync();
}