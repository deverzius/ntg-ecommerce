using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Update;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description,
    Guid? ParentCategoryId
) : IRequest;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken
    )
    {
        var canCauseCircularCategories = await categoryRepository.AnyAsync(
            c => c.ParentCategoryId == command.Id && c.Id == command.ParentCategoryId,
            cancellationToken
        );

        if (canCauseCircularCategories)
        {
            throw new AppException(400, "Circular dependency detected in category hierarchy.");
        }

        var category = new Category
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            ParentCategoryId = command.ParentCategoryId
        };

        categoryRepository.Update(category);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}