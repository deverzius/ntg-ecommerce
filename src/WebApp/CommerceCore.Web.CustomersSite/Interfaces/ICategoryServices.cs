namespace CommerceCore.Web.CustomersSite.Interfaces;

public interface ICategoryServices
{
    Task<PaginatedListViewModel<SimpleCategoryViewModel>> FetchCategories();
}
