using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.ViewModels;

public class HomePageViewModel
{
    public required PagedResult<ProductResponse> Products { get; set; }
    public required PagedResult<CategoryResponse> Categories { get; set; }
}
