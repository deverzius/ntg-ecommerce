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
