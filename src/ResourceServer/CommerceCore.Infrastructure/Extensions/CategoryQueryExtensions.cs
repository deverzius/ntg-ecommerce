using CommerceCore.Domain.Entities;

namespace CommerceCore.Infrastructure.Extensions;

public static class CategoryQueryExtensions
{
    public static IQueryable<Category> SortBy(this IQueryable<Category> source, string? sortBy)
    {
        source = sortBy?.ToLower() switch
        {
            _ => source.OrderBy(p => p.Id)
        };

        return source;
    }

    public static IQueryable<Category> SearchBy(this IQueryable<Category> source, string? searchPhrase)
    {
        if (string.IsNullOrEmpty(searchPhrase)) return source;

        var lowerCaseTerm = searchPhrase.Trim().ToLower();

        return source.Where(p => p.Name.ToLower().Contains(lowerCaseTerm)
        );
    }

    public static IQueryable<Category> FilterBy(this IQueryable<Category> source, Guid? categoryId)
    {
        return source;
    }
}
