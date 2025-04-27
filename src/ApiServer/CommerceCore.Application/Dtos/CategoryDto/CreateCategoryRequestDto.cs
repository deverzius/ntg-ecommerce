using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.CategoryDto;

public class CreateCategoryRequestDto() : IRequestDto<Category>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public Category ToModelInstance()
    {
        return new Category
        {
            Name = Name,
            Description = Description,
            ParentCategoryId = ParentCategoryId,
        };
    }
}

