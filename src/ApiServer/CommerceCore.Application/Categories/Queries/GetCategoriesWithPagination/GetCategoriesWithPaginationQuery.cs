using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;

public record GetCategoriesWithPaginationQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<CategoryResponseDto>> { }
