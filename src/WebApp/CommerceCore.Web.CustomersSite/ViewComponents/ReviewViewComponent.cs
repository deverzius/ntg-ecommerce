using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class ReviewViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ReviewViewModel review)
    {
        return View(review);
    }
}
