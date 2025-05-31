using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Create;
using CommerceCore.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    ICollection<CreateProductVariantRequest> VariantRequests
) : IRequest;

public class UpdateProduct(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);
        if (category is null) throw new AppException(404, "Category not found");

        var product = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            UpdatedDate = DateTime.UtcNow,
            Category = category
        };

        productRepository.Update(product);

        try
        {
            await unitOfWork.SaveAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new AppException(500, "Error when update Product", exception.ToString());
        }
    }
}
