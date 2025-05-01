using MediatR;

namespace CommerceCore.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await _context.Products.FindAsync([request.Id], cancellationToken);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
