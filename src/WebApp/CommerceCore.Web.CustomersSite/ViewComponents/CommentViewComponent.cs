using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class CommentViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
