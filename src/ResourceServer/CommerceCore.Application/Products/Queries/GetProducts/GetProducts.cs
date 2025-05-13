using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Queries.GetProducts;

public record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? CategoryId = null
) : IRequest<PaginatedResponse<ProductResponse>>;

public class GetProducts(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, PaginatedResponse<ProductResponse>>
{
    public async Task<PaginatedResponse<ProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = repository.GetQueryableList();

        var result = await query
            .AsNoTracking()
            .Where(p => !request.CategoryId.HasValue || p.CategoryId == request.CategoryId)
            .Select(p => p.ToDto())
            .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }
}
