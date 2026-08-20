using Application.Dtos;
using Application.Interfaces;
using Application.Shared;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Result<Guid>> Add(ProductDto productDto)
    {
        var validationResult = ValidateProductDto<Guid>(productDto);
        if (!validationResult?.IsSuccess ?? false)
        {
            return validationResult;
        }

        var product = productRepository.Add(productDto);
        await productRepository.SaveChangesAsync();
        return Result<Guid>.Success(product.Id);
    }

    public async Task<Result<ProductDto>> GetById(Guid id)
    {
        var product = await productRepository.GetById(id);
        if (product is not null)
        {
            var productDto = new ProductDto(product.Name, product.Price, product.StockQuantity, product.Description);
            return Result<ProductDto>.Success(productDto);
        }

        return Result<ProductDto>.NotFound($"Product with id '{id}' is not exist.");
    }

    public async Task<Result<List<Product>>> GetListPaginated(PaginationParams paginationParams)
    {
        var products = await productRepository.GetListPaginated(paginationParams);
        return Result<List<Product>>.Success(products);
    }

    public async Task<Result<string>> Update(Guid id, ProductDto productDto)
    {
        var product = await productRepository.GetById(id);
        if (product is null)
        {
            return Result<string>.NotFound($"Product with id '{id}' is not exist.");
        }

        var validationResult = ValidateProductDto<string>(productDto);
        if (!validationResult?.IsSuccess ?? false)
        {
            return validationResult;
        }

        await productRepository.Update(id, productDto);

        await productRepository.SaveChangesAsync();
        return Result<string>.Success(null);
    }

    private Result<T>? ValidateProductDto<T>(ProductDto productDto)
    {
        if (productDto.Name.Length < 3)
        {
            return Result<T>.Fail("Product name length should be 3 characters at least.");
        }

        if (productDto.Name.Length > 150)
        {
            return Result<T>.Fail("Product name length can't be more than 150 characters");
        }

        if (productDto.Price < 0)
        {
            return Result<T>.Fail("Product price can't be negative.");
        }

        if (productDto.StockQuantity < 0)
        {
            return Result<T>.Fail("Product quantity can't be negative.");
        }

        if (!productDto.Description.IsNullOrEmpty())
        {
            if (productDto.Description?.Length < 3)
            {
                return Result<T>.Fail("Product description length should be 3 characters at least.");
            }

            if (productDto.Description?.Length > 400)
            {
                return Result<T>.Fail("Product description length can't be more than 400 characters");
            }
        }
        
        return null;
    }
}