using MediatR;

namespace CommerceCore.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid BrandId,
    Guid CategoryId
) : IRequest<bool> { }
