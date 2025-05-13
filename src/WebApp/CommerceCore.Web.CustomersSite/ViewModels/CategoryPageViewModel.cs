using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

public class CategoryPageViewModel
{
    public CategoryResponse Category { get; set; }
    public PaginatedResponse<ProductResponse> Products { get; set; }
}