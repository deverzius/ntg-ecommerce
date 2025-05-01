using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<ProductResponseDto>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandId { get; set; }
}
