using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Queries.Get;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class CartsController : ControllerBase
{
    [HttpGet("my-cart")]
    [Authorize]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart(
        ISender sender
    )
    {
        var userId = User.ExtractUserId();

        var query = new GetMyCartQuery(userId);
        var result = await sender.Send(query);

        if (result is null)
        {
            var command = new CreateMyCartCommand(userId);
            result = await sender.Send(command);
        }

        return Ok(result);
    }

    [HttpPost("my-cart/Items")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostCartItems(
        ISender sender,
        AddItemsToCartCommand command
    )
    {
        var userId = User.ExtractUserId();

        var result = await sender.Send(command);

        return Created();
    }
}