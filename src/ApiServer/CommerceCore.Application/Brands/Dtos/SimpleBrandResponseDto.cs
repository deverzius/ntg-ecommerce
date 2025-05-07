namespace CommerceCore.Application.Brands.Dtos;

public class SimpleBrandResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
