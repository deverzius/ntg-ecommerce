using MediatR;

namespace CommerceCore.Application.Products.Commands.UpdateProduct;

public record UpdateProductImageRecord(string Name, string Path);

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid BrandId,
    Guid CategoryId,
    UpdateProductImageRecord[] Images
) : IRequest<bool> { }
