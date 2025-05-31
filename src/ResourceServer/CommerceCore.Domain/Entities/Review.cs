using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Review
{
    public Guid Id { get; init; }
    public required int Rating { get; init; }
    public required string Title { get; init; }
    public required string Comment { get; init; }
    public DateTime CreatedDate { get; init; }
    public required Guid ProductId { get; init; }

    public string? FullName { get; init; }

    // Required when UserId is null
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
}
