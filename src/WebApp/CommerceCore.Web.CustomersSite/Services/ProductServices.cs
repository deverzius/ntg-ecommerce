using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using Microsoft.AspNetCore.WebUtilities;

public class ProductServices(HttpClient client, IConfiguration config) : IProductServices
{
    private readonly string _apiUrl = config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    public async Task<PaginatedResponse<ProductResponse>> FetchProducts()
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
            var products = JsonSerializer.Deserialize<PaginatedResponse<ProductResponse>>(
                json,
                JsonHelper.Options
            );

            return products ?? new PaginatedResponse<ProductResponse>([], 0, 0, 0, false, false);
        }
        catch
        {
            return new PaginatedResponse<ProductResponse>([], 0, 0, 0, false, false);
        }
    }

    public async Task<ProductResponse?> FetchProductById(Guid id)
    {
        try
        {
            var url = _apiUrl + "/v1/products/" + id;

            var response = await client.GetAsync(
                new Uri(url)
            );

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductResponse>(
                json,
                JsonHelper.Options
            );

            return product;
        }
        catch
        {
            return null;
        }
    }
}
