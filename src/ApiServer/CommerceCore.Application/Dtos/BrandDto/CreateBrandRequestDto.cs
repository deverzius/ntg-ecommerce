using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.BrandDto;

public class CreateBrandRequestDto() : IRequestDto<Brand>
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    public Brand ToModelInstance()
    {
        return new Brand
        {
            Name = Name,
            Description = Description,
        };
    }
}
