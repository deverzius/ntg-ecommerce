using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Categories.Dtos;

public class CategoryResponseDto(Category category)
{
    public Guid Id => category.Id;
    public string Name => category.Name;
    public string Description => category.Description;
    public Guid? ParentCategoryId => category.ParentCategoryId;
    public Category? ParentCategory => category.ParentCategory;
    public ICollection<Product> Products => category.Products;
}
