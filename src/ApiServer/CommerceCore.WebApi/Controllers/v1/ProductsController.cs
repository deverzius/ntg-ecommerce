﻿using CommerceCore.Application.Products.Commands.CreateProduct;
using CommerceCore.Application.Products.Commands.CreateReview;
using CommerceCore.Application.Products.Commands.DeleteProduct;
using CommerceCore.Application.Products.Commands.UpdateProduct;
using CommerceCore.Application.Products.Queries.GetProduct;
using CommerceCore.Application.Products.Queries.GetProductsWithPagination;
using CommerceCore.Application.Products.Queries.GetReviewsByProductId;
using CommerceCore.WebApi.Shared.Mappings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController(IConfiguration configuration) : ControllerBase
{
    private readonly string _publicStorageUrl =
        $"{configuration["Supabase:StorageBaseUrl"]}/storage/v1/object/public/{configuration["Supabase:BucketName"]}/"
        ?? throw new ArgumentNullException(
            "Supabase:StorageBaseUrl or Supabase:BucketName is null"
        );

    [HttpGet]
    [ProducesResponseType(
        typeof(Ok<PaginatedListViewModel<ProductViewModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<Ok<PaginatedListViewModel<ProductViewModel>>> GetProductsWithPagination(
        ISender sender,
        [FromQuery] GetProductsWithPaginationQuery query
    )
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result.ToViewModel(_publicStorageUrl));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Ok<ProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<ProductWithReviewsViewModel>, NotFound>> GetProduct(
        ISender sender,
        Guid id
    )
    {
        var product = await sender.Send(new GetProductQuery(id));

        if (product == null)
            return TypedResults.NotFound();

        var reviews = await sender.Send(new GetReviewsByProductIdQuery(id));

        return TypedResults.Ok(
            new ProductWithReviewsViewModel
            {
                Product = product.ToViewModel(_publicStorageUrl),
                Reviews = [.. reviews.Select(r => r.ToViewModel())],
            }
        );
    }

    [HttpPost("{id}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<ReviewViewModel>, BadRequest<string>>> PostReview(
        ISender sender,
        CreateReviewCommand command,
        Guid id
    )
    {
        if (command.ProductId != id)
            return TypedResults.BadRequest("Product Id does not match.");

        var review = await sender.Send(command);

        if (review == null)
            return TypedResults.BadRequest("Failed to create review.");

        return TypedResults.Ok(review.ToViewModel());
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(Created<ProductViewModel>), StatusCodes.Status201Created)]
    public async Task<Created<ProductViewModel>> PostProduct(
        ISender sender,
        CreateProductCommand command
    )
    {
        var result = await sender.Send(command);

        return TypedResults.Created(nameof(GetProduct), result.ToViewModel(_publicStorageUrl));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
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
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<NoContent, NotFound>> DeleteProduct(ISender sender, Guid id)
    {
        var result = await sender.Send(new DeleteProductCommand(id));

        return result ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
