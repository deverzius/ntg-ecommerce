using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCategoriesQuery, PaginatedList<CategoryResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<CategoryResponse>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Categories.Select(c => c.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
