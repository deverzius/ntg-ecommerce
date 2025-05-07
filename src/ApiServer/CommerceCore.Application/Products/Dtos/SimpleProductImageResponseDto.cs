namespace CommerceCore.Application.Products.Dtos;

public class SimpleProductImageResponseDto
{
    public required string Name { get; set; }
    public required string Path { get; set; }
    public required Guid ProductId { get; set; }
}
