using System;
using System.Collections.Generic;

public class BrandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProductResponse> Products { get; set; }
}