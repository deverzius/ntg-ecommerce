namespace CommerceCore.SharedViewModels;

public class PaginatedListViewModel<T>()
{
    public IReadOnlyCollection<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
