using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<CategoryResponse>>
{
}