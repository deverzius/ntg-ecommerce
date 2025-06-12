using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateCategoryCommand(string Name, string Description, Guid? ParentCategoryId)
    : IRequest<CategoryResponse>;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId
        };

        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);

        return category.ToDto();
    }
}
