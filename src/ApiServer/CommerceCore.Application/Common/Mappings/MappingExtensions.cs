using CommerceCore.Application.Common.Models;

namespace CommerceCore.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
        where TDestination : class
    {
        return PaginatedList<TDestination>.CreateAsync(
            source,
            pageNumber,
            pageSize,
            cancellationToken
        );
    }
}
