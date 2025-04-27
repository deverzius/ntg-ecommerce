using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.CategoryDto;

public class CategoryResponseDto(Category category)
{
    private readonly Category _category = category;

    public Guid Id => _category.Id;
    public string Name => _category.Name;
    public string Description => _category.Description;
    public Guid? ParentCategoryId => _category.ParentCategoryId;
    public virtual Category? ParentCategory => _category.ParentCategory;
    public virtual ICollection<Product> Products => _category.Products;
}
