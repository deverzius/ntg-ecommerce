using CommerceCore.Application.Categories.Dtos;
using MediatR;

namespace CommerceCore.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<CategoryResponseDto>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
