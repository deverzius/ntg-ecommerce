using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.CategoryDto;

public class UpdateCategoryRequestDto() : IRequestDto<Category>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public Category ToModelInstance()
    {
        return new Category
        {
            Id = Id,
            Name = Name,
            Description = Description,
            ParentCategoryId = ParentCategoryId,
        };
    }
}
