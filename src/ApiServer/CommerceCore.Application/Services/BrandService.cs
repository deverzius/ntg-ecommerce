using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.BrandDto;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommerceCore.Application.Services;

public class BrandService(DbContext context, ILogger<BrandService> logger) : IBrandService
{
    private readonly DbContext _context = context;
    private readonly DbSet<Brand> _brandRepository = context.Set<Brand>();
    private readonly ILogger<BrandService> _logger = logger;

    public async Task<IEnumerable<BrandResponseDto>> GetAllAsync()
    {
        try
        {
            return await _brandRepository.Select(x => new BrandResponseDto(x)).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(BrandService)} error: Failed to perform {nameof(GetAllAsync)} method.");
            return [];
        }
    }

    public async Task<BrandResponseDto?> GetByIdAsync(Guid id)
    {
        return await _brandRepository
            .FirstOrDefaultAsync(x => x.Id == id)
            .ContinueWith(x => x.Result == null ? null : new BrandResponseDto(x.Result));
    }

    public async Task<BrandResponseDto?> CreateAsync(CreateBrandRequestDto brandDto)
    {
        var result = _brandRepository.Add(brandDto.ToModelInstance());

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(BrandService)} error: Failed to perform {nameof(CreateAsync)} method.");
            return null;
        }

        return new BrandResponseDto(result.Entity);
    }

    public async Task<BrandResponseDto?> UpdateAsync(Guid id, UpdateBrandRequestDto brandDto)
    {
        if (id != brandDto.Id)
        {
            _logger.LogWarning($"{nameof(BrandService)} warning: Failed to perform {nameof(UpdateAsync)} method due to Id mismatch.");
            return null;
        }

        var brand = brandDto.ToModelInstance();
        var brandEntry = _context.Entry(brand);
        brandEntry.State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(BrandService)} error: Failed to perform {nameof(UpdateAsync)} method.");
            return null;
        }

        return new BrandResponseDto(brandEntry.Entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var brand = await _brandRepository.FindAsync(id);

        if (brand == null)
        {
            _logger.LogWarning($"{nameof(BrandService)} warning: Failed to perform {nameof(DeleteAsync)} method due to {nameof(Brand)} not found.");
            return false;
        }

        _brandRepository.Remove(brand);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(BrandService)} error: Failed to perform {nameof(DeleteAsync)} method.");
            return false;
        }

        return true;
    }
}