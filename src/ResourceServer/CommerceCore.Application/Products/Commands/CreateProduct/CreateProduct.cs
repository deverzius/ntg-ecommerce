using Ardalis.GuardClauses;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Create;
using CommerceCore.Shared.DTOs.Responses;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    ICollection<CreateProductVariantRequest> VariantRequests
) : IRequest<ProductResponse>;

public class CreateProduct(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);
        if (category is null) throw new AppException(404, "Category not found");

        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = category
        };

        await productRepository.AddAsync(product, cancellationToken);

        List<ProductVariant> variants = [];
        variants.AddRange(command.VariantRequests.Select(variantRequest => new ProductVariant
        {
            Name = variantRequest.Name,
            Value = variantRequest.Value,
            StockQuantity = variantRequest.StockQuantity,
            Product = product
        }));

        product.Variants = variants;

        await unitOfWork.SaveAsync(cancellationToken);

        return product.ToDto();
    }
}
