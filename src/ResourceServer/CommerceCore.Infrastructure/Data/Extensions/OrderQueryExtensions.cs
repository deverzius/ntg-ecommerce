using CommerceCore.Domain.Entities;

namespace CommerceCore.Infrastructure.Data.Extensions;

public static class OrderQueryExtensions
{
    public static IQueryable<Order> SortBy(this IQueryable<Order> source, string? sortBy)
    {
        source = sortBy?.ToLower() switch
        {
            _ => source.OrderBy(p => p.Id)
        };

        return source;
    }

    public static IQueryable<Order> SearchBy(this IQueryable<Order> source, string? searchPhrase)
    {
        if (string.IsNullOrEmpty(searchPhrase)) return source;

        var lowerCaseTerm = searchPhrase.Trim().ToLower();

        return source;
    }

    public static IQueryable<Order> FilterBy(this IQueryable<Order> source)
    {
        return source;
    }
}
