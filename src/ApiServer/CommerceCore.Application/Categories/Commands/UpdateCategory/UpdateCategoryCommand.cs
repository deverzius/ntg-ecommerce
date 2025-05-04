using MediatR;

namespace CommerceCore.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description,
    Guid? ParentCategoryId
) : IRequest<bool> { }
