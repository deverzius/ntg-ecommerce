using Microsoft.AspNetCore.Mvc;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.ProductDto;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, UpdateProductRequestDto productDto)
    {
        var updatedProduct = await _productService.UpdateAsync(id, productDto);

        if (updatedProduct == null)
        {
            return BadRequest();
        }

        return Ok(updatedProduct);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> PostProduct(CreateProductRequestDto productDto)
    {
        var createdProductDto = await _productService.CreateAsync(productDto);

        if (createdProductDto == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetProduct), new { id = createdProductDto.Id }, createdProductDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var isDeleted = await _productService.DeleteAsync(id);

        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

