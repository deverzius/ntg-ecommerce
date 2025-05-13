namespace CommerceCore.Shared.DTOs.Common;

public record PaginatedResponse<T>(
    IReadOnlyList<T> Items,
    int PageNumber,
    int TotalPages,
    int TotalCount,
    bool HasPreviousPage,
    bool HasNextPage
);
