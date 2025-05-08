using CommerceCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.Images.Length > 3)
        {
            return false;
        }

        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            UpdatedDate = DateTime.UtcNow,
            BrandId = request.BrandId,
            CategoryId = request.CategoryId,
        };
        var productEntry = _context.Products.Entry(product);
        productEntry.State = EntityState.Modified;

        // Avoid modifying the CreatedDate property during update
        productEntry.Property(x => x.CreatedDate).IsModified = false;

        // Handle images
        _context.ProductImages.RemoveRange(
            _context.ProductImages.Where(pi => pi.ProductId == request.Id)
        );
        _context.ProductImages.AddRange(
            request.Images.Select(image => new ProductImage
            {
                Name = image.Name,
                Path = image.Path,
                ProductId = request.Id,
            })
        );

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
