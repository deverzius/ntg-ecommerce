using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class ProductResponseDto(Product product)
{
    public Guid Id => product.Id;
    public string Name => product.Name;
    public string Description => product.Description;
    public decimal Price => product.Price;
    public Guid BrandId => product.BrandId;
    public Guid CategoryId => product.CategoryId;
    public DateTime CreatedDate => product.CreatedDate;
    public DateTime UpdatedDate => product.UpdatedDate;

    // When firstly created, _product.Brand and _product.Category are null
    public SimpleBrandResponseDto? Brand => product.Brand == null ? null : new(product.Brand);
    public SimpleCategoryResponseDto? Category =>
        product.Category == null ? null : new(product.Category);
    public ICollection<SimpleProductImageResponseDto> Images =>
        [.. product.Images.Select(i => new SimpleProductImageResponseDto(i))];
    public ICollection<ProductVariant> Variants => product.Variants;
}
