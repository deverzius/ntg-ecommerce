using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? CategoryId = null
) : IRequest<PaginatedList<ProductResponseDto>> { }
