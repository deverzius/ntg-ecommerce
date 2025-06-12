using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.ViewModels;

public class CategoryPageViewModel
{
    public required CategoryResponse Category { get; set; }
    public required PagedResult<ProductResponse> Products { get; set; }
}