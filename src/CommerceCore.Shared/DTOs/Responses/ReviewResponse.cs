using System;

namespace CommerceCore.Shared.DTOs.Responses;

public record ReviewResponse(
    Guid Id,
    int Rating,
    string Title,
    string Comment,
    DateTime CreatedDate,
    string FullName,
    string PhoneNumber,
    string Email
);
