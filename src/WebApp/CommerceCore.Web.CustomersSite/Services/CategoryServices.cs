using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.Shared.Helpers;

public class CategoryServices(HttpClient client, IConfiguration config) : ICategoryServices
{
    private readonly string _apiUrl = config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    public async Task<PaginatedListViewModel<SimpleCategoryViewModel>> FetchCategories()
    {
        try
        {
            var response = await client.GetAsync(
                _apiUrl + "/v1/categories/?PageNumber=1&PageSize=100"
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<
                PaginatedListViewModel<SimpleCategoryViewModel>
            >(json, JsonHelper.Options);

            return categories ?? new PaginatedListViewModel<SimpleCategoryViewModel>();
        }
        catch
        {
            return new PaginatedListViewModel<SimpleCategoryViewModel>();
        }
    }
}
