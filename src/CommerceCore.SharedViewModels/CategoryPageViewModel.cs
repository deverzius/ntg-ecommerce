public class CategoryPageViewModel
{
    public SimpleCategoryViewModel Category { get; set; }
    public PaginatedListViewModel<ProductViewModel> Products { get; set; }
}
