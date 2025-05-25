namespace CommerceCore.Shared.DTOs.Common;

public record PagedResult<T>(
    IReadOnlyList<T> Items,
    int PageNumber,
    int PageSize,
    int TotalPages
);
