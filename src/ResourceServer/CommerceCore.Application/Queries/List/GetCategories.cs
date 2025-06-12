using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
using MediatR;

namespace CommerceCore.Application.Queries.List;

public class GetCategoriesQuery : PagedResultRequest, IRequest<PagedResult<CategoryResponse>>
{
    public string? CategoryId { get; init; }
    public string? ParentCategoryId { get; init; }
}

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
{
    public async Task<PagedResult<CategoryResponse>> Handle(
        GetCategoriesQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await categoryRepository.GetPagedResultAsync(query, cancellationToken);

        return new PagedResult<CategoryResponse>(
            result.Items.Select(c => c.ToDto()).ToList(),
            result.PageNumber,
            result.PageSize,
            result.TotalPages
        );
    }
}
