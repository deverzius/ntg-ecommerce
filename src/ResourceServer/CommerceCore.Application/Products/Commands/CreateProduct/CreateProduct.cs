using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    Guid BrandId,
    Guid CategoryId
) : IRequest<ProductResponse>;

public class CreateProduct(IApplicationDbContext context)
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
