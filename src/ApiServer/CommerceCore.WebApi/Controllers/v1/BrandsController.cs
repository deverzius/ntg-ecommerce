using CommerceCore.Application.Brands.Queries.GetBrand;
using CommerceCore.Application.Brands.Queries.GetBrandsWithPagination;
using CommerceCore.Application.Common.Models;
using CommerceCore.WebApi.Shared.Mappings;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BrandsController() : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(
        typeof(Ok<PaginatedListViewModel<SimpleBrandViewModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<Ok<PaginatedListViewModel<SimpleBrandViewModel>>> GetBrandsWithPagination(
        ISender sender,
        [FromQuery] GetBrandsWithPaginationQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result.ToSimpleViewModel());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Ok<SimpleBrandViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<SimpleBrandViewModel>, NotFound>> GetBrand(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetBrandQuery(id));

        return result == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(result.ToSimpleViewModel());
    }
}
