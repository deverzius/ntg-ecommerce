using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommerceCore.Application.Services;

public class ProductService(DbContext context, ILogger<ProductService> logger) : ISingleModelService<Product, Guid>
{
    private readonly DbContext _context = context;
    private readonly DbSet<Product> _productRepository = context.Set<Product>();
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            return await _productRepository.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform ${nameof(GetAllAsync)} method.");
            return [];
        }
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _productRepository.FindAsync(id);
    }

    public async Task<Product?> CreateAsync(Product product)
    {
        _productRepository.Add(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform ${nameof(CreateAsync)} method.");
            return null;
        }

        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, Product product)
    {
        if (id != product.Id)
        {
            _logger.LogWarning($"{nameof(ProductService)} warning: Failed to perform ${nameof(UpdateAsync)} method due to Id mismatch.");
            return null;
        }

        _productRepository.Update(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform ${nameof(UpdateAsync)} method.");
            return null;
        }

        return product;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.FindAsync(id);

        if (product == null)
        {
            _logger.LogWarning($"{nameof(ProductService)} warning: Failed to perform ${nameof(DeleteAsync)} method due to product not found.");
            return false;
        }

        _productRepository.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(ProductService)} error: Failed to perform ${nameof(UpdateAsync)} method.");
            return false;
        }

        return true;
    }
}