using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class ProductResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandId { get; set; }
    public required Guid CategoryId { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }

    // When firstly created, _product.Brand and _product.Category are null
    public SimpleBrandResponseDto? Brand { get; set; }
    public SimpleCategoryResponseDto? Category { get; set; }
    public ICollection<SimpleProductImageResponseDto> Images { get; set; } = [];
    public ICollection<ProductVariant> Variants { get; set; } = [];
}
