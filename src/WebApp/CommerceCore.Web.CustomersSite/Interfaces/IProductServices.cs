using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Web.CustomersSite.Interfaces;

public interface IProductServices
{
    Task<PagedResult<ProductResponse>> FetchProducts();
    Task<ProductResponse?> FetchProductById(Guid id);
}
