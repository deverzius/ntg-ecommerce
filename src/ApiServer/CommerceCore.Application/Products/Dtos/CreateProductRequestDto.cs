using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Products.Dtos;

public class CreateProductRequestDto() : IRequestDto<Product>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandId { get; set; }

    public Product ToModelInstance()
    {
        return new Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            BrandId = BrandId,
        };
    }
}
