using System.Diagnostics;
using CommerceCore.Web.CustomersSite.Interfaces;
using CommerceCore.Web.CustomersSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.Controllers;

public class HomeController(
    IProductServices productServices,
    ICategoryServices categoryServices
) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await productServices.FetchProducts();
        var categories = await categoryServices.FetchCategories();

        return View(new HomePageViewModel { Products = products, Categories = categories });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
