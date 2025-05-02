using CommerceCore.Application.Categories.Commands.CreateCategory;
using CommerceCore.Application.Categories.Commands.DeleteCategory;
using CommerceCore.Application.Categories.Commands.UpdateCategory;
using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;
using CommerceCore.Application.Categories.Queries.GetCategory;
using CommerceCore.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController() : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Ok<PaginatedList<CategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Ok<PaginatedList<CategoryResponseDto>>> GetCategoriesWithPagination(
        ISender sender,
        [FromQuery] GetCategoriesWithPaginationQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Ok<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<CategoryResponseDto>, NotFound>> GetCategory(
        ISender sender,
        Guid id
    )
    {
        var result = await sender.Send(new GetCategoryQuery(id));

        return result == null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Created<CategoryResponseDto>), StatusCodes.Status201Created)]
    public async Task<Created<CategoryResponseDto>> PostCategory(
        ISender sender,
        CreateCategoryCommand command
    )
    {
        var result = await sender.Send(command);

        return TypedResults.Created(nameof(GetCategory), result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<NoContent, BadRequest, NotFound>> PutCategory(
        ISender sender,
        Guid id,
        UpdateCategoryCommand command
    )
    {
        if (id != command.Id)
            return TypedResults.BadRequest();

        var result = await sender.Send(command);

        return result ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<NoContent, NotFound>> DeleteCategory(ISender sender, Guid id)
    {
        var result = await sender.Send(new DeleteCategoryCommand(id));

        return result ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
