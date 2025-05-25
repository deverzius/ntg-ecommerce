using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.ViewModels;

public class HomePageViewModel
{
    public PagedResult<ProductResponse> Products { get; set; }
    public PagedResult<CategoryResponse> Categories { get; set; }
}
