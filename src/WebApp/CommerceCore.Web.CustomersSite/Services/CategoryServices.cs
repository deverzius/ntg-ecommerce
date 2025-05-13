using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.Shared.Helpers;

namespace CommerceCore.Web.CustomersSite.Services;

public class CategoryServices(HttpClient client, IConfiguration config) : ICategoryServices
{
    private readonly string _apiUrl = config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    public async Task<PaginatedResponse<CategoryResponse>> FetchCategories()
    {
        try
        {
            var response = await client.GetAsync(
                _apiUrl + "/v1/categories/?PageNumber=1&PageSize=100"
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<
                PaginatedResponse<CategoryResponse>
            >(json, JsonHelper.Options);

            return categories ?? new PaginatedResponse<CategoryResponse>([], 0, 0, 0, false, false);
        }
        catch
        {
            return new PaginatedResponse<CategoryResponse>([], 0, 0, 0, false, false);
        }
    }
}
