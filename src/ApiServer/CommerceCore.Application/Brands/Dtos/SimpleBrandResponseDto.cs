using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Brands.Dtos;

public class SimpleBrandResponseDto(Brand brand)
{
    private readonly Brand _brand = brand;

    public Guid Id => _brand.Id;
    public string Name => _brand.Name;
    public string Description => _brand.Description;
}
