using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class CommentListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
