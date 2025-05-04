using CommerceCore.Application.Categories.Dtos;
using MediatR;

namespace CommerceCore.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description, Guid? ParentCategoryId)
    : IRequest<CategoryResponseDto> { }
