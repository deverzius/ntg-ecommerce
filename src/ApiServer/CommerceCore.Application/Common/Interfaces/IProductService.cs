using CommerceCore.Application.Dtos.ProductDto;

namespace CommerceCore.Application.Common.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto?> CreateAsync(CreateProductRequestDto productDto);
    Task<ProductResponseDto?> UpdateAsync(Guid id, UpdateProductRequestDto productDto);
    Task<bool> DeleteAsync(Guid id);
}