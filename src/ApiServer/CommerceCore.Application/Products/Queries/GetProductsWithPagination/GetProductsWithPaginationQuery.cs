using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductResponseDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
