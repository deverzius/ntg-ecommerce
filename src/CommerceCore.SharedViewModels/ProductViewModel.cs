using System;
using System.Collections.Generic;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public SimpleBrandViewModel Brand { get; set; } = null;
    public SimpleCategoryViewModel Category { get; set; } = null;
    public List<SimpleProductImageViewModel> Images { get; set; }
}
