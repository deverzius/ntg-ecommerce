namespace CommerceCore.SharedViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public SimpleBrandViewModel? Brand { get; set; }
    public SimpleCategoryViewModel? Category { get; set; }
    public List<SimpleProductImageViewModel> Images { get; set; }
}
