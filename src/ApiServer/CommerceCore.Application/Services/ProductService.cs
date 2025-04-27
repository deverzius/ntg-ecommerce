using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.ProductDto;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommerceCore.Application.Services;

public class ProductService(DbContext context, ILogger<ProductService> logger) : IProductService
{
    private readonly DbContext _context = context;
    private readonly DbSet<Product> _productRepository = context.Set<Product>();
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        try
        {
            return await _productRepository.Select(x => new ProductResponseDto(x)).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform {nameof(GetAllAsync)} method.");
            return [];
        }
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        return await _productRepository
            .FirstOrDefaultAsync(x => x.Id == id)
            .ContinueWith(x => x.Result == null ? null : new ProductResponseDto(x.Result));
    }

    public async Task<ProductResponseDto?> CreateAsync(CreateProductRequestDto productDto)
    {
        var result = _productRepository.Add(productDto.ToModelInstance());

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform {nameof(CreateAsync)} method.");
            return null;
        }

        return new ProductResponseDto(result.Entity);
    }

    public async Task<ProductResponseDto?> UpdateAsync(Guid id, UpdateProductRequestDto productDto)
    {
        if (id != productDto.Id)
        {
            _logger.LogWarning($"{nameof(ProductService)} warning: Failed to perform {nameof(UpdateAsync)} method due to Id mismatch.");
            return null;
        }

        var product = productDto.ToModelInstance();
        var productEntry = _context.Entry(product);
        productEntry.State = EntityState.Modified;

        // Avoid modifying the CreatedDate property during update
        productEntry.Property(x => x.CreatedDate).IsModified = false;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform {nameof(UpdateAsync)} method.");
            return null;
        }

        return new ProductResponseDto(productEntry.Entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.FindAsync(id);

        if (product == null)
        {
            _logger.LogWarning($"{nameof(ProductService)} warning: Failed to perform {nameof(DeleteAsync)} method due to product not found.");
            return false;
        }

        _productRepository.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform {nameof(UpdateAsync)} method.");
            return false;
        }

        return true;
    }
}