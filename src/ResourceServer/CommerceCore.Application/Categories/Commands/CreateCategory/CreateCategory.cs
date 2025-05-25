using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description, Guid? ParentCategoryId)
    : IRequest<CategoryResponse>;

public class CreateCategoryCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly IApplicationDbContext _context = context;

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

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.ToDto();
    }
}
