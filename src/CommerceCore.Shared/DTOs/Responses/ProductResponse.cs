using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace CommerceCore.Shared.DTOs.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid BrandId,
    Guid CategoryId,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    List<string> Images
);
