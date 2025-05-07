using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Categories.Dtos;

public class CategoryResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
