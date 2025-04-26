using Microsoft.AspNetCore.Mvc;
using CommerceCore.Domain.Entities;
using CommerceCore.Application.Common.Interfaces;

namespace CommerceCore.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController(ISingleModelService<Product, Guid> productService) : ControllerBase
    {
        private readonly ISingleModelService<Product, Guid> _productService = productService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            var updatedProduct = await _productService.UpdateAsync(id, product);

            if (updatedProduct == null)
            {
                return BadRequest();
            }

            return Ok(updatedProduct);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var createdProduct = await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var isDeleted = await _productService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
