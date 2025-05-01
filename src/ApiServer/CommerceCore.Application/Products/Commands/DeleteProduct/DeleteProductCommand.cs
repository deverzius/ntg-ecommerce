using MediatR;

namespace CommerceCore.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<bool> { }
