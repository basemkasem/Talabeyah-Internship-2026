using Application.Dtos;
using Application.Shared;
using Domain.Models;

namespace Application.Services;

public interface IProductService
{
    Task<Result<Guid>> Add(ProductDto productDto);
    Task<Result<ProductDto>> GetById(Guid id);
    Task<Result<List<Product>>> GetListPaginated(PaginationParams paginationParams);
    Task<Result<string>> Update(Guid id, ProductDto productDto);
}