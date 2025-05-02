using MediatR;

namespace CommerceCore.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await _context.Categories.FindAsync([request.Id], cancellationToken);
        if (category == null)
        {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
