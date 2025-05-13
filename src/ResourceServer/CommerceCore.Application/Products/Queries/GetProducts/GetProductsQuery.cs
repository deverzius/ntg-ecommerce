using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? CategoryId = null
) : IRequest<PaginatedList<ProductResponse>>
{
}
