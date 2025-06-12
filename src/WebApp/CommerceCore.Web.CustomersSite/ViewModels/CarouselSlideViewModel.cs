namespace CommerceCore.Web.CustomersSite.ViewModels;

public class CarouselSlideViewModel
{
    public required string ImgSrc { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string CtaUrl { get; set; }
    public required string CaptionPosition { get; set; }
    public bool IsDarkText { get; set; }
}