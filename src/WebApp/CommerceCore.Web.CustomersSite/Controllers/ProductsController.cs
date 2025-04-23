using Microsoft.AspNetCore.Mvc;

namespace CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class ProductsController(ILogger<ProductsController> logger) : Controller
{
    [HttpGet("{id}")]
    public IActionResult Details(string id)
    {
        return View();
    }
}

