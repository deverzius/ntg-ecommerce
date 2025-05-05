using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class SimpleProductImageResponseDto(ProductImage productImage)
{
    public string Name => productImage.Name;
    public string Path => productImage.Path;
    public Guid ProductId => productImage.ProductId;
}
