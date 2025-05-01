using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Commands.CreateProduct;
using CommerceCore.Application.Products.Commands.DeleteProduct;
using CommerceCore.Application.Products.Commands.UpdateProduct;
using CommerceCore.Application.Products.Dtos;
using CommerceCore.Application.Products.Queries.GetProduct;
using CommerceCore.Application.Products.Queries.GetProductsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController() : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Ok<PaginatedList<ProductResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Ok<PaginatedList<ProductResponseDto>>> GetProductsWithPagination(
        ISender sender,
        [FromQuery] GetProductsWithPaginationQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Ok<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<ProductResponseDto>, NotFound>> GetProduct(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetProductQuery(id));

        return result == null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Created<ProductResponseDto>), StatusCodes.Status201Created)]
    public async Task<Created<ProductResponseDto>> PostProduct(
        ISender sender,
        CreateProductCommand command
    )
    {
        var result = await sender.Send(command);

        return TypedResults.Created(nameof(GetProduct), result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<NoContent, BadRequest, NotFound>> PutProduct(
        ISender sender,
        Guid id,
        UpdateProductCommand command
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
    public async Task<Results<NoContent, NotFound>> DeleteProduct(ISender sender, Guid id)
    {
        var result = await sender.Send(new DeleteProductCommand(id));

        return result ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
