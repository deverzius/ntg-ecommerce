using CustomersSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomersSite.ViewComponents;

public class CommentViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
