using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.Interfaces;

public interface ICategoryServices
{
    Task<PaginatedResponse<CategoryResponse>> FetchCategories();
}
