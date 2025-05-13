using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrands;

public record GetBrandsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<BrandResponse>>
{
}