using System.Text.Json;
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
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await FetchProductById(id);

        return View(product);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> CreateComment(ReviewViewModel reviewModel, Guid productId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                _apiUrl + "/v1/products/" + productId + "/reviews",
                new ReviewRequestViewModel
                {
                    Rating = reviewModel.Rating,
                    Title = reviewModel.Title,
                    Comment = reviewModel.Comment,
                    ProductId = productId,
                    FullName = reviewModel.FullName,
                    PhoneNumber = reviewModel.PhoneNumber,
                    Email = reviewModel.Email,
                }
            );

            if (response.IsSuccessStatusCode)
            {
                return Redirect(
                    $"/Product/Details/{productId}?formSuccess={Uri.EscapeDataString("Review created successfully.")}"
                );
            }

            return Redirect(
                $"/Product/Details/{productId}?formError={Uri.EscapeDataString(response.RequestMessage.ToString())}"
            );
        }
        catch (Exception ex)
        {
            return Redirect(
                $"/Product/Details/{productId}?formError={Uri.EscapeDataString(ex.Message.ToString())}"
            );
        }
    }

    private async Task<ProductWithReviewsViewModel?> FetchProductById(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl + "/v1/products/" + id);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductWithReviewsViewModel>(
                json,
                JsonHelper.Options
            );

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex}", ex.Message);
            return null;
        }
    }
}
