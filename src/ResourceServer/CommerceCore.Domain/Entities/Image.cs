namespace CommerceCore.Domain.Entities;

public class Image
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string Type { get; init; }
    public DateTime UploadedDate { get; init; }
    public ICollection<string> Tags { get; init; } = [];
}
