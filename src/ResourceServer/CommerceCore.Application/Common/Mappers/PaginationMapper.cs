using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Common.Mappers;

public static class PaginationMapper
{
    public static async Task<PagedResult<T>> PaginateAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    ) where T : class
    {
        var totalItems = await source.CountAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(items, pageNumber, pageSize, totalPages);
    }
}
