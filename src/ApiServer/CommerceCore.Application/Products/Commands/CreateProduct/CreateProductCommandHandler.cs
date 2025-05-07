using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Products.Dtos;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductResponseDto> Handle(
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
            CategoryId = request.CategoryId,
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
