using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class ProductController(
    ILogger<ProductController> logger,
    HttpClient httpClient,
    IConfiguration config
) : Controller
{
    private readonly ILogger<ProductController> _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiUrl =
        config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(string id)
    {
        var product = await FetchProductById(id);

        return View(product);
    }

    private async Task<ProductViewModel?> FetchProductById(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl + "/v1/products/" + id);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductViewModel>(json, JsonHelper.Options);

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
}
