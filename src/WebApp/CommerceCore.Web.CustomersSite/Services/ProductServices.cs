using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using Microsoft.AspNetCore.WebUtilities;

public class ProductServices(HttpClient client, IConfiguration config) : IProductServices
{
    private readonly string _apiUrl = config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    public async Task<PaginatedListViewModel<ProductViewModel>> FetchProducts()
    {
        try
        {
            var url = _apiUrl + "/v1/products";
            var queryParams = new Dictionary<string, string>
            {
                { "PageNumber", "1" },
                { "PageSize", "16" }
            };

            var response = await client.GetAsync(
                new Uri(QueryHelpers.AddQueryString(url, queryParams))
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<PaginatedListViewModel<ProductViewModel>>(
                json,
                JsonHelper.Options
            );

            return products ?? new PaginatedListViewModel<ProductViewModel>();
        }
        catch
        {
            return new PaginatedListViewModel<ProductViewModel>();
        }
    }
}
