using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class ProductResponseDto(Product product)
{
    private readonly Product _product = product;

    public Guid Id => _product.Id;
    public string Name => _product.Name;
    public string Description => _product.Description;
    public decimal Price => _product.Price;
    public Guid BrandId => _product.BrandId;
    public Guid CategoryId => _product.CategoryId;
    public DateTime CreatedDate => _product.CreatedDate;
    public DateTime UpdatedDate => _product.UpdatedDate;

    // When firstly created, _product.Brand and _product.Category are null
    public SimpleBrandResponseDto? Brand => _product.Brand == null ? null : new(_product.Brand);
    public SimpleCategoryResponseDto? Category =>
        _product.Category == null ? null : new(_product.Category);
    public ICollection<SimpleProductImageResponseDto> Images =>
        [.. _product.Images.Select(i => new SimpleProductImageResponseDto(i))];
    public ICollection<ProductVariant> Variants => _product.Variants;
    public ICollection<Review> Reviews => _product.Reviews;
}
