using System.Diagnostics;
using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using CommerceCore.Web.CustomersSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    HttpClient httpClient,
    IConfiguration config
) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiUrl =
        config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    public async Task<IActionResult> Index()
    {
        var products = await FetchProducts();
        var categories = await FetchCategories();

        return View(new HomePageViewModel { Products = products, Categories = categories });
    }

    private async Task<PaginatedListViewModel<ProductViewModel>> FetchProducts()
    {
        try
        {
            var response = await _httpClient.GetAsync(
                _apiUrl + "/v1/products/?PageNumber=1&PageSize=100"
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<PaginatedListViewModel<ProductViewModel>>(
                json,
                JsonHelper.Options
            );

            return products ?? new();
        }
        catch
        {
            return new PaginatedListViewModel<ProductViewModel>();
        }
    }

    private async Task<PaginatedListViewModel<SimpleCategoryViewModel>> FetchCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync(
                _apiUrl + "/v1/categories/?PageNumber=1&PageSize=100"
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<
                PaginatedListViewModel<SimpleCategoryViewModel>
            >(json, JsonHelper.Options);

            return categories ?? new();
        }
        catch
        {
            return new PaginatedListViewModel<SimpleCategoryViewModel>();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
