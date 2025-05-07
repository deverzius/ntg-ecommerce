using CommerceCore.Application.Categories.Commands.CreateCategory;
using CommerceCore.Application.Categories.Commands.DeleteCategory;
using CommerceCore.Application.Categories.Commands.UpdateCategory;
using CommerceCore.Application.Categories.Queries.GetCategoriesWithPagination;
using CommerceCore.Application.Categories.Queries.GetCategory;
using CommerceCore.WebApi.Shared.Mappings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController() : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(
        typeof(Ok<PaginatedListViewModel<SimpleCategoryViewModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<
        Ok<PaginatedListViewModel<SimpleCategoryViewModel>>
    > GetCategoriesWithPagination(
        ISender sender,
        [FromQuery] GetCategoriesWithPaginationQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result.ToViewModel());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Ok<SimpleCategoryViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<SimpleCategoryViewModel>, NotFound>> GetCategory(
        ISender sender,
        Guid id
    )
    {
        var result = await sender.Send(new GetCategoryQuery(id));

        return result == null ? TypedResults.NotFound() : TypedResults.Ok(result.ToViewModel());
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(Created<SimpleCategoryViewModel>), StatusCodes.Status201Created)]
    public async Task<Created<SimpleCategoryViewModel>> PostCategory(
        ISender sender,
        CreateCategoryCommand command
    )
    {
        var result = await sender.Send(command);

        return TypedResults.Created(nameof(GetCategory), result.ToViewModel());
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<NoContent, BadRequest>> PutCategory(
        ISender sender,
        Guid id,
        UpdateCategoryCommand command
    )
    {
        if (id != command.Id)
            return TypedResults.BadRequest();

        if (id == command.ParentCategoryId)
            return TypedResults.BadRequest();

        var result = await sender.Send(command);

        return result ? TypedResults.NoContent() : TypedResults.BadRequest();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<NoContent, BadRequest>> DeleteCategory(ISender sender, Guid id)
    {
        var result = await sender.Send(new DeleteCategoryCommand(id));

        return result ? TypedResults.NoContent() : TypedResults.BadRequest();
    }
}
