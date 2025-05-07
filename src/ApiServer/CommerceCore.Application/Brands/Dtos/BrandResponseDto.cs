using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Brands.Dtos;

public class BrandResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
