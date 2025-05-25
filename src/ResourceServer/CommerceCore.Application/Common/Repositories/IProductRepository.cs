using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Products.Queries.GetProducts;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;

namespace CommerceCore.Application.Common.Repositories;

public interface IProductRepository : IWriteRepository<Product>
{
    Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
