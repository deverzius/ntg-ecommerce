using CommerceCore.Application.Dtos.BrandDto;

namespace CommerceCore.Application.Common.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<BrandResponseDto>> GetAllAsync();
    Task<BrandResponseDto?> GetByIdAsync(Guid id);
    Task<BrandResponseDto?> CreateAsync(CreateBrandRequestDto brandDto);
    Task<BrandResponseDto?> UpdateAsync(Guid id, UpdateBrandRequestDto brandDto);
    Task<bool> DeleteAsync(Guid id);
}