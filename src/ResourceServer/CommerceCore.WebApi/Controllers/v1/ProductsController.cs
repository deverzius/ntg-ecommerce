using CommerceCore.Application.Products.Commands.Create;
using CommerceCore.Application.Products.Commands.CreateReview;
using CommerceCore.Application.Products.Commands.DeleteProduct;
using CommerceCore.Application.Products.Commands.UpdateProduct;
using CommerceCore.Application.Products.Queries.GetProduct;
using CommerceCore.Application.Products.Queries.GetProducts;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Create;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class ProductsController(IConfiguration configuration) : ControllerBase
{
    // private readonly string _publicStorageUrl =
    //     $"{configuration["Supabase:StorageBaseUrl"]}/storage/v1/object/public/{configuration["Supabase:BucketName"]}/"
    //     ?? throw new ArgumentNullException(
    //         "Supabase:StorageBaseUrl or Supabase:BucketName is null"
    //     );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        ISender sender,
        [FromQuery] GetProductsQuery query
    )
    {
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProduct(
        ISender sender,
        Guid id
    )
    {
        var product = await sender.Send(new GetProductQuery(id));
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostReview(
        ISender sender,
        CreateReviewCommand command,
        Guid id
    )
    {
        if (command.ProductId != id) return BadRequest("Product Id does not match.");

        var review = await sender.Send(command);

        return Ok(review);
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostProduct(
        ISender sender,
        CreateProductRequest productRequest
    )
    {
        var command = new CreateProductCommand(productRequest);
        var result = await sender.Send(command);

        return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutProduct(
        ISender sender,
        Guid id,
        UpdateProductCommand command
    )
    {
        if (id != command.Id) return BadRequest();

        await sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProduct(ISender sender, Guid id)
    {
        await sender.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
