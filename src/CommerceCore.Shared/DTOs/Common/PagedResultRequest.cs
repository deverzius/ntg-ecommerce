namespace CommerceCore.Shared.DTOs.Common;

public abstract class PagedResultRequest
{
    public string? Search { get; init; }
    public string? Sort { get; init; }
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;
}
