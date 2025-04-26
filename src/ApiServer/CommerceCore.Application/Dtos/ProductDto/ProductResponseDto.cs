using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.ProductDto;

// Composition approach to create ProductResponseDto
public class ProductResponseDto(Product product)
{
    private readonly Product _product = product;

    public Guid Id => _product.Id;
    public string Name => _product.Name;
    public string Description => _product.Description;
    public decimal Price => _product.Price;
    public Guid BrandId => _product.BrandId;
    public DateTime CreatedDate => _product.CreatedDate;
    public DateTime UpdatedDate => _product.UpdatedDate;
    public virtual Brand Brand => _product.Brand;
    public virtual ICollection<ProductImage> Images => _product.Images;
    public virtual ICollection<ProductVariant> Variants => _product.Variants;
    public virtual ICollection<Category> Categories => _product.Categories;
    public virtual ICollection<Review> Reviews => _product.Reviews;
}
