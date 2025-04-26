using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.ProductDto;

public class UpdateProductRequestDto() : IRequestDto<Product>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
    public required Guid BrandId { get; set; }

    public Product ToModelInstance()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            CreatedDate = CreatedDate,
            UpdatedDate = UpdatedDate,
            BrandId = BrandId,
        };
    }
}
