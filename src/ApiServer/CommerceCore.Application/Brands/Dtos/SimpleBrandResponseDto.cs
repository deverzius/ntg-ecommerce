using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Brands.Dtos;

public class SimpleBrandResponseDto(Brand brand)
{
    public Guid Id => brand.Id;
    public string Name => brand.Name;
    public string Description => brand.Description;
}
