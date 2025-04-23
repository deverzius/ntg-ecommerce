using CommerceCore.Web.CustomersSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class SvgIconViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name, int? width, int? height, int size = 24, string strokeWidth = "1.25")
    {
        var icon = new SvgIconViewModel { Name = name, Height = height ?? size, Width = width ?? size, StrokeWidth = strokeWidth };
        return View(icon);
    }
}
