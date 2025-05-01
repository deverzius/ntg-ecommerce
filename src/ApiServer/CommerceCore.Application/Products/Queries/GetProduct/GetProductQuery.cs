using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ProductResponseDto?> { }
