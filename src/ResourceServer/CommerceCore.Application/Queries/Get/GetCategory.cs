using CommerceCore.Application.Common.Interfaces.Repositories;
using MediatR;

namespace CommerceCore.Application.Queries.Get;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryResponse?>;

public class GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryQuery, CategoryResponse?>
{
    public async Task<CategoryResponse?> Handle(
        GetCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await categoryRepository.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        return result?.ToDto();
    }
}
