using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.CategoryDto;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommerceCore.Application.Services;

public class CategoryService(DbContext context, ILogger<CategoryService> logger) : ICategoryService
{
    private readonly DbContext _context = context;
    private readonly DbSet<Category> _categoryRepository = context.Set<Category>();
    private readonly ILogger<CategoryService> _logger = logger;

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        try
        {
            return await _categoryRepository.Select(x => new CategoryResponseDto(x)).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CategoryService)} error: Failed to perform {nameof(GetAllAsync)} method.");
            return [];
        }
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        return await _categoryRepository
            .FirstOrDefaultAsync(x => x.Id == id)
            .ContinueWith(x => x.Result == null ? null : new CategoryResponseDto(x.Result));
    }

    public async Task<CategoryResponseDto?> CreateAsync(CreateCategoryRequestDto categoryDto)
    {
        var result = _categoryRepository.Add(categoryDto.ToModelInstance());

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CategoryService)} error: Failed to perform {nameof(CreateAsync)} method.");
            return null;
        }

        return new CategoryResponseDto(result.Entity);
    }

    public async Task<CategoryResponseDto?> UpdateAsync(Guid id, UpdateCategoryRequestDto categoryDto)
    {
        if (id != categoryDto.Id)
        {
            _logger.LogWarning($"{nameof(CategoryService)} warning: Failed to perform {nameof(UpdateAsync)} method due to Id mismatch.");
            return null;
        }

        var category = categoryDto.ToModelInstance();
        var categoryEntry = _context.Entry(category);
        categoryEntry.State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CategoryService)} error: Failed to perform {nameof(UpdateAsync)} method.");
            return null;
        }

        return new CategoryResponseDto(categoryEntry.Entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.FindAsync(id);

        if (category == null)
        {
            _logger.LogWarning($"{nameof(CategoryService)} warning: Failed to perform {nameof(DeleteAsync)} method due to {nameof(Category)} not found.");
            return false;
        }

        _categoryRepository.Remove(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CategoryService)} error: Failed to perform {nameof(DeleteAsync)} method.");
            return false;
        }

        return true;
    }
}