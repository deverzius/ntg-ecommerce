using MediatR;

namespace CommerceCore.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<bool>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandId { get; set; }
    public required Guid CategoryId { get; set; }
}
