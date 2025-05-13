using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductResponse> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            BrandId = request.BrandId,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
