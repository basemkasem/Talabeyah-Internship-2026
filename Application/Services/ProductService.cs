using Application.Common;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Application.Shared;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Result<Guid>> Add(ProductDto productDto)
    {
        var dtoValidationResult = ValidateProductDto<Guid>(productDto);
        if (!dtoValidationResult?.IsSuccess ?? false)
            return dtoValidationResult;

        var product = productRepository.Add(productDto);
        await productRepository.SaveChangesAsync();
        return product.Id;
    }

    public async Task<Result<ProductDto>> GetById(Guid id)
    {
        var product = await productRepository.GetById(id);
        if (product is null)
            return ProductErrors.ProductNotFound(id);

        var productDto = new ProductDto(product.Name, product.Price, product.StockQuantity, product.Description);
        return productDto;
    }

    public async Task<Result<List<Product>>> GetListPaginated(PaginationParams paginationParams)
    {
        var products = await productRepository.GetListPaginated(paginationParams);
        return products;
    }

    public async Task<Result> Update(Guid id, ProductDto productDto)
    {
        var product = await productRepository.GetById(id);
        if (product is null)
        {
            return ProductErrors.ProductNotFound(id);
        }

        var validationResult = ValidateProductDto<string>(productDto);
        if (!validationResult?.IsSuccess ?? false)
        {
            return validationResult;
        }

        await productRepository.Update(id, productDto);

        await productRepository.SaveChangesAsync();
        return Result.Success();
    }

    private Result<T>? ValidateProductDto<T>(ProductDto productDto)
    {
        if (productDto.Name.Length < 3)
        {
            return ProductErrors.NameTooShort();
        }

        if (productDto.Name.Length > 150)
        {
            return ProductErrors.NameExceedMaxLength();
        }

        if (productDto.Price < 0)
        {
            return ProductErrors.PriceIsNegative();
        }

        if (productDto.StockQuantity < 0)
        {
            return ProductErrors.StockQuantityIsNegative();
        }

        if (!productDto.Description.IsNullOrEmpty())
        {
            if (productDto.Description?.Length < 3)
            {
                return ProductErrors.DescriptionTooShort();
            }

            if (productDto.Description?.Length > 400)
            {
                return ProductErrors.DescriptionExceedMaxLength();
            }
        }

        return null;
    }
}