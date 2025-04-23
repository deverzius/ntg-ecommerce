using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class ProductsController(ILogger<ProductsController> logger) : Controller
{
    [HttpGet("{id}")]
    public IActionResult Details(string id)
    {
        return View();
    }
}

