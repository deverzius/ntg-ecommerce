using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrandsWithPagination;

public record GetBrandsWithPaginationQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<BrandResponseDto>> { }
