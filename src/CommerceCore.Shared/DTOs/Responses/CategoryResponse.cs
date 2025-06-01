using System;

namespace CommerceCore.Shared.DTOs.Responses;

public record CategoryResponse
(
    Guid Id,
    string Name,
    string Description,
    Guid? ParentCategoryId,
    CategoryResponse? ParentCategory,
    DateTime CreatedDate,
    DateTime UpdatedDate
);

