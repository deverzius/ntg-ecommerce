using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Category
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }

    public virtual Category? ParentCategory { get; init; }
    public virtual AppImage? Image { get; init; }
}
