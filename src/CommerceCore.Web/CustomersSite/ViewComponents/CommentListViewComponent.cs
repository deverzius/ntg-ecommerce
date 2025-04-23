using Microsoft.AspNetCore.Mvc;

namespace CustomersSite.ViewComponents;

public class CommentListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
