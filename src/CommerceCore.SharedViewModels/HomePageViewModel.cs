public class HomePageViewModel
{
    public PaginatedListViewModel<ProductViewModel> Products { get; set; }
    public PaginatedListViewModel<SimpleCategoryViewModel> Categories { get; set; }
}
