using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryResponseDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
