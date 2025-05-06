using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

[Route("[controller]/[action]")]
public class ProductController(ILogger<ProductController> logger) : Controller
{
    [HttpGet("{id}")]
    public IActionResult Details(string id)
    {
        return View();
    }
}
