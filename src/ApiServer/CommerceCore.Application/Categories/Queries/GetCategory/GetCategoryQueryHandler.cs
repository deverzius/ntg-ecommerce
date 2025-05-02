using CommerceCore.Application.Categories.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCategoryQuery, CategoryResponseDto?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CategoryResponseDto?> Handle(
        GetCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Categories.FirstOrDefaultAsync(
            p => p.Id == request.Id,
            cancellationToken
        );

        return result == null ? null : new CategoryResponseDto(result);
    }
}
