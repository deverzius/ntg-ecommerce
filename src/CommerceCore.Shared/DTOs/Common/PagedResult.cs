namespace CommerceCore.Shared.DTOs.Common;

public record PagedResult<T>(
    ICollection<T> Items,
    int PageNumber,
    int PageSize,
    int TotalPages
);
