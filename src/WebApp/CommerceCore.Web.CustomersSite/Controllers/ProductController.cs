using System.Text.Json;
using Ardalis.GuardClauses;
using CommerceCore.Shared.DTOs.Responses;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class ProductController(
    ILogger<ProductController> logger,
    HttpClient httpClient,
    IConfiguration config,
    IProductServices productServices
) : Controller
{
    private readonly string _apiUrl =
        config["API:BaseUrl"] ?? Guard.Against.NullOrEmpty(config["API:BaseUrl"]);

    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ProductController> _logger = logger;
    private readonly IProductServices _productServices = productServices;

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _productServices.FetchProductById(id);

        return View(product);
    }

    // [HttpPost("{productId}")]
    // public async Task<IActionResult> CreateComment(ReviewResponse review, Guid productId)
    // {
    //     try
    //     {
    //         var response = await _httpClient.PostAsJsonAsync(
    //             _apiUrl + "/v1/products/" + productId + "/reviews",
    //             new ReviewRequestViewModel
    //             {
    //                 Rating = review.Rating,
    //                 Title = review.Title,
    //                 Comment = review.Comment,
    //                 ProductId = productId,
    //                 FullName = review.FullName,
    //                 PhoneNumber = review.PhoneNumber,
    //                 Email = review.Email
    //             }
    //         );

    //         if (response.IsSuccessStatusCode)
    //             return Redirect(
    //                 $"/Product/Details/{productId}?formSuccess={Uri.EscapeDataString("Review created successfully.")}"
    //             );

    //         return Redirect(
    //             $"/Product/Details/{productId}?formError={Uri.EscapeDataString(response.RequestMessage.ToString())}"
    //         );
    //     }
    //     catch (Exception ex)
    //     {
    //         return Redirect(
    //             $"/Product/Details/{productId}?formError={Uri.EscapeDataString(ex.Message)}"
    //         );
    //     }
    // }
}
