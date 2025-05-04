using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class SimpleProductImageResponseDto(ProductImage productImage)
{
    private readonly ProductImage _productImage = productImage;

    public string Name => _productImage.Name;
    public string Path => _productImage.Path;
    public Guid ProductId => _productImage.ProductId;
}
