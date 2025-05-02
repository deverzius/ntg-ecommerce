using CommerceCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        // Avoid circular dependency
        var circularCategories = await _context
            .Categories.Where(c =>
                c.ParentCategoryId == request.Id && c.Id == request.ParentCategoryId
            )
            .ToListAsync(cancellationToken);
        if (circularCategories.Count > 0)
        {
            return false;
        }

        var category = new Category
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId,
        };
        var categoryEntry = _context.Categories.Entry(category);
        categoryEntry.State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

        return true;
    }
}
