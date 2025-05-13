using CommerceCore.Shared.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ProductResponse product)
    {
        return View(product);
    }
}