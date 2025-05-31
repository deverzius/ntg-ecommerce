using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryResponse?>;

public class GetCategory(IApplicationDbContext context)
    : IRequestHandler<GetCategoryQuery, CategoryResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CategoryResponse?> Handle(
        GetCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Categories.FirstOrDefaultAsync(
            p => p.Id == request.Id,
            cancellationToken
        );

        return result?.ToDto();
    }
}
