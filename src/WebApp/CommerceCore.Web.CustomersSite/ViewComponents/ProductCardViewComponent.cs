using Microsoft.AspNetCore.Mvc;

namespace CustomersSite.ViewComponents;

public class ProductCardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
