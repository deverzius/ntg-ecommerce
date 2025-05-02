using MediatR;

namespace CommerceCore.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest<bool>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
