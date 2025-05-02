using MediatR;

namespace CommerceCore.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<bool> { }
