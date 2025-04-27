using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Dtos.BrandDto;

public class UpdateBrandRequestDto() : IRequestDto<Brand>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public Brand ToModelInstance()
    {
        return new Brand
        {
            Id = Id,
            Name = Name,
            Description = Description
        };
    }
}


