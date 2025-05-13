using CommerceCore.Application.Categories.Commands.CreateCategory;
using CommerceCore.Application.Categories.Commands.DeleteCategory;
using CommerceCore.Application.Categories.Commands.UpdateCategory;
using CommerceCore.Application.Categories.Queries.GetCategories;
using CommerceCore.Application.Categories.Queries.GetCategory;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(
        ISender sender,
        [FromQuery] GetCategoriesQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(
        ISender sender,
        Guid id
    )
    {
        var result = await sender.Send(new GetCategoryQuery(id));

        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostCategory(
        ISender sender,
        CreateCategoryCommand command
    )
    {
        var result = await sender.Send(command);

        return Created(nameof(GetCategory), result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutCategory(
        ISender sender,
        Guid id,
        UpdateCategoryCommand command
    )
    {
        if (id != command.Id)
            return BadRequest();

        if (id == command.ParentCategoryId)
            return BadRequest();

        var result = await sender.Send(command);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(ISender sender, Guid id)
    {
        var result = await sender.Send(new DeleteCategoryCommand(id));

        return result ? NoContent() : BadRequest();
    }
}
