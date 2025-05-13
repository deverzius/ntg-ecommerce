using CommerceCore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;

public class DeleteCategory(IApplicationDbContext context)
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await _context.Categories.FindAsync([request.Id], cancellationToken);
        if (category == null) return false;

        var isParentCategory = await _context.Categories.AnyAsync(
            c => c.ParentCategoryId == category.Id,
            cancellationToken
        );
        if (isParentCategory) return false;

        var areSomeProductsInCategory = _context.Products.Any(p => p.CategoryId == request.Id);
        if (areSomeProductsInCategory) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}