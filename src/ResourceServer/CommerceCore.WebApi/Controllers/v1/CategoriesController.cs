using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Commands.Delete;
using CommerceCore.Application.Commands.Update;
using CommerceCore.Application.Queries.Get;
using CommerceCore.Application.Queries.List;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CategoryResponse>), StatusCodes.Status200OK)]
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
    public async Task<IActionResult> GetCategory(
        ISender sender,
        Guid id
    )
    {
        var result = await sender.Send(new GetCategoryQuery(id));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
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

        return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutCategory(
        ISender sender,
        Guid id,
        UpdateCategoryCommand command
    )
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        if (id == command.ParentCategoryId)
        {
            return BadRequest();
        }

        await sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory(ISender sender, Guid id)
    {
        await sender.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}
