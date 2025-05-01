using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrandsWithPagination;

public record GetBrandsWithPaginationQuery : IRequest<PaginatedList<BrandResponseDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
