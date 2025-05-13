namespace CommerceCore.Web.CustomersSite.Interfaces;

public interface IProductServices
{
    Task<PaginatedListViewModel<ProductViewModel>> FetchProducts();
}
