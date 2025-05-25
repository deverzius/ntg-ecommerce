using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PagedResult<CategoryResponse>>;

public class GetCategories(IApplicationDbContext context)
    : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PagedResult<CategoryResponse>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Categories.Select(c => c.ToDto())
            .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
