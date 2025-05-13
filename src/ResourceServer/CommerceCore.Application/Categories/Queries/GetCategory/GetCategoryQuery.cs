using MediatR;

namespace CommerceCore.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryResponse?>
{
}
