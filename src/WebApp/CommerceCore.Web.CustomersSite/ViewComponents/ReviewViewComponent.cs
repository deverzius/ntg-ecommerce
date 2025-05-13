using CommerceCore.Shared.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Web.CustomersSite.ViewComponents;

public class ReviewViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ReviewResponse review)
    {
        return View(review);
    }
}