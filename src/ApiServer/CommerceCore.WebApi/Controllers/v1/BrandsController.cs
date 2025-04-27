using Microsoft.AspNetCore.Mvc;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.BrandDto;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BrandsController(IBrandService brandService) : ControllerBase
{
    private readonly IBrandService _brandService = brandService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandResponseDto>>> GetBrands()
    {
        return Ok(await _brandService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandResponseDto>> GetBrand(Guid id)
    {
        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return Ok(brand);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBrand(Guid id, UpdateBrandRequestDto brandDto)
    {
        var updatedBrand = await _brandService.UpdateAsync(id, brandDto);

        if (updatedBrand == null)
        {
            return BadRequest();
        }

        return Ok(updatedBrand);
    }

    [HttpPost]
    public async Task<ActionResult<BrandResponseDto>> PostBrand(CreateBrandRequestDto brandDto)
    {
        var createdBrandDto = await _brandService.CreateAsync(brandDto);

        if (createdBrandDto == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBrand), new { id = createdBrandDto.Id }, createdBrandDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(Guid id)
    {
        var isDeleted = await _brandService.DeleteAsync(id);

        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

