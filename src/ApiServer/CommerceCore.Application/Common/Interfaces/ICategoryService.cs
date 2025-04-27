using CommerceCore.Application.Dtos.CategoryDto;

namespace CommerceCore.Application.Common.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(Guid id);
    Task<CategoryResponseDto?> CreateAsync(CreateCategoryRequestDto categoryDto);
    Task<CategoryResponseDto?> UpdateAsync(Guid id, UpdateCategoryRequestDto categoryDto);
    Task<bool> DeleteAsync(Guid id);
}