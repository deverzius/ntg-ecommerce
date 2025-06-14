using System.ComponentModel.DataAnnotations;

namespace CommerceCore.Domain.Entities;

public class Review
{
    public Guid Id { get; init; }

    [Range(1, 5)]
    public required int Rating { get; init; }
    public required string Title { get; init; }
    public required string Comment { get; init; }
    public DateTime CreatedDate { get; init; }
    public required Guid ProductId { get; init; }
    public string? FullName { get; init; }

    [Phone]
    public string? PhoneNumber { get; init; }

    [EmailAddress]
    public string? Email { get; init; }
}
