using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.GetByIdAsync(
            request.Id,
            cancellationToken
        );
        if (product == null)
        {
            throw new AppException(404, $"Product with ID {request.Id} not found.");
        }

        productRepository.Remove(product);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}