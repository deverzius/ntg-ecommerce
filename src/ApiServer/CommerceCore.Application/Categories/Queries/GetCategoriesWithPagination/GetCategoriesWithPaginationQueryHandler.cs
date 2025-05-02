using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;

public class GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryResponseDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<CategoryResponseDto>> Handle(
        GetCategoriesWithPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Categories.Select(c => new CategoryResponseDto(c))
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
