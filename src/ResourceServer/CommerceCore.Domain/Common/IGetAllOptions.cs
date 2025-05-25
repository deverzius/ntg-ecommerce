namespace CommerceCore.Domain.Common;

public abstract class IGetAllOptions
{
    public string? Search { get; set; }
    public string? Filter { get; set; }
    public string? SortBy { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
