namespace CommerceCore.Application.Files.Dtos;

public class FileUrlDto
{
    public string? Path { get; set; }
    public required string SignedURL { get; set; }
}