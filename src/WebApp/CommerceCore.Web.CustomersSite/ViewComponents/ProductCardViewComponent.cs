using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class ProductCardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ProductViewModel product)
    {
        return View(product);
    }
}
