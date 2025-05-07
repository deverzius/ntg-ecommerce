namespace CommerceCore.Application.Categories.Dtos;

public class SimpleCategoryResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
