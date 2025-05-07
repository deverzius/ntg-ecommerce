using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class ProductViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ProductViewModel product)
    {
        return View(product);
    }
}
