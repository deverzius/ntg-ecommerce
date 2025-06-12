using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Delete;

public record DeleteCategoryCommand(Guid Id) : IRequest;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(
            request.Id,
            cancellationToken
        );
        if (category == null)
        {
            throw new AppException(404, "Category not found.");
        }

        var isParentCategory = await categoryRepository.AnyAsync(
            c => c.ParentCategoryId == category.Id,
            cancellationToken
        );
        if (isParentCategory)
        {
            throw new AppException(400, "Cannot delete a category that has subcategories.");
        }

        var areSomeProductsInCategory = await productRepository.AnyAsync(
            p => p.Category.Id == request.Id,
            cancellationToken
        );
        if (areSomeProductsInCategory)
        {
            throw new AppException(400, "Cannot delete a category that has products.");
        }

        categoryRepository.Remove(category);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}
