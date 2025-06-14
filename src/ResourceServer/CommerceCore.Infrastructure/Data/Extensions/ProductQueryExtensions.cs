using CommerceCore.Domain.Entities;

namespace CommerceCore.Infrastructure.Data.Extensions;

public static class ProductQueryExtensions
{
    public static IQueryable<Product> SortBy(this IQueryable<Product> source, string? sortBy)
    {
        source = sortBy?.ToLower() switch
        {
            _ => source.OrderBy(p => p.Id)
        };

        return source;
    }

    public static IQueryable<Product> SearchBy(this IQueryable<Product> source, string? searchPhrase)
    {
        if (string.IsNullOrEmpty(searchPhrase)) return source;

        var lowerCaseTerm = searchPhrase.Trim().ToLower();

        return source.Where(p => p.Name.ToLower().Contains(lowerCaseTerm)
        );
    }

    public static IQueryable<Product> FilterBy(this IQueryable<Product> source, Guid? categoryId)
    {
        if (categoryId != null) source = source.Where(p => p.Category.Id == categoryId);

        return source;
    }
}
