namespace CommerceCore.Application.Products.Dtos;

public class ReviewResponseDto
{
    public required Guid Id { get; set; }
    public required int Rating { get; set; }
    public required string Title { get; set; }
    public required string Comment { get; set; }
    public required DateTime CreatedDate { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
