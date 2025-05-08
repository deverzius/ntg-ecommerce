using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SimpleCategoryViewModel product)
    {
        return View(product);
    }
}
