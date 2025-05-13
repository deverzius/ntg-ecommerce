using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrand;

public record GetBrandQuery(Guid Id) : IRequest<BrandResponse?>
{
}
