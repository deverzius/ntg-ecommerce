using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateCategoryCommand, CategoryResponseDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CategoryResponseDto> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId,
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return new CategoryResponseDto(category);
    }
}
