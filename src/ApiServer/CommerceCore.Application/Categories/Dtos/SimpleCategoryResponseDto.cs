using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Categories.Dtos;

public class SimpleCategoryResponseDto(Category category)
{
    private readonly Category _category = category;

    public Guid Id => _category.Id;
    public string Name => _category.Name;
    public string Description => _category.Description;
    public Guid? ParentCategoryId => _category.ParentCategoryId;
}
