using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.ViewModels;

public class HomePageViewModel
{
    public PaginatedResponse<ProductResponse> Products { get; set; }
    public PaginatedResponse<CategoryResponse> Categories { get; set; }
}
