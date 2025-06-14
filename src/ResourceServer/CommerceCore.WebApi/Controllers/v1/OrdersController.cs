using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.DTOs;
using CommerceCore.Application.Queries.List;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(PagedResult<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders(
        ISender sender,
        [FromQuery] GetOrdersQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("my-orders")]
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyOrders(
        ISender sender,
        [FromQuery] GetOrdersQuery query
    )
    {
        var myOrdersQuery = new GetMyOrdersQuery
        {
            UserId = User.ExtractUserId(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            Sort = query.Sort,
            Search = query.Search
        };

        var result = await sender.Send(myOrdersQuery);

        return Ok(result);
    }

    [HttpGet("my-orders/{id:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyOrder(
        ISender sender,
        Guid id
    )
    {
        var query = new GetMyOrderQuery(User.ExtractUserId(), id);
        var result = await sender.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("my-orders")]
    [Authorize]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostOrder(
        ISender sender,
        CreateOrderDTO order
    )
    {
        var command = new CreateOrderCommand(User.ExtractUserId(), order);
        var result = await sender.Send(command);

        return CreatedAtAction(nameof(GetMyOrder), new { id = result.Id }, result);
    }
}