using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Common.Mappings;

public static class PaginationMappingExtensions
{
    public static async Task<PaginatedResponse<T>> PaginateAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    ) where T : class
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var hasPreviousPage = pageNumber > 1;

        var hasNextPage = pageSize < totalPages;

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResponse<T>(items, pageNumber, totalPages, totalCount, hasPreviousPage, hasNextPage);
    }
}
