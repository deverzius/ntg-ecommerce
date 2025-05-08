using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class CategoryController(
    ILogger<CategoryController> logger,
    HttpClient httpClient,
    IConfiguration config
) : Controller
{
    private readonly ILogger<CategoryController> _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiUrl =
        config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    [HttpGet("products")]
    public async Task<IActionResult> Products([FromQuery] Guid CategoryId)
    {
        var products = await FetchProducts(CategoryId);
        var category = await FetchCategoryById(CategoryId);

        Console.WriteLine(JsonSerializer.Serialize(products));

        return View(new CategoryPageViewModel { Category = category, Products = products });
    }

    private async Task<PaginatedListViewModel<ProductViewModel>> FetchProducts(Guid CategoryId)
    {
        try
        {
            var url = _apiUrl + "/v1/products";
            var queryParams = new Dictionary<string, string>()
            {
                { "PageNumber", "1" },
                { "PageSize", "8" },
                { "CategoryId", CategoryId.ToString() },
            };

            var response = await _httpClient.GetAsync(
                new Uri(QueryHelpers.AddQueryString(url, queryParams))
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<PaginatedListViewModel<ProductViewModel>>(
                json,
                JsonHelper.Options
            );

            return products ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex}", ex.Message);
            return new PaginatedListViewModel<ProductViewModel>();
        }
    }

    private async Task<SimpleCategoryViewModel?> FetchCategoryById(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl + "/v1/categories/" + id);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<SimpleCategoryViewModel>(
                json,
                JsonHelper.Options
            );

            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex}", ex.Message);
            return null;
        }
    }
}
