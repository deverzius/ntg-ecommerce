using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    Guid BrandId,
    Guid CategoryId
) : IRequest<ProductResponseDto> { }
