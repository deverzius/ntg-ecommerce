using CommerceCore.Application.Brands.Queries.GetBrand;
using CommerceCore.Application.Brands.Queries.GetBrands;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BrandsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<BrandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrands(
        ISender sender,
        [FromQuery] GetBrandsQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBrand(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetBrandQuery(id));

        return result == null ? NotFound() : Ok(result);
    }
}
