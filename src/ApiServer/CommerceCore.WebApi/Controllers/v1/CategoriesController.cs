using Microsoft.AspNetCore.Mvc;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Dtos.CategoryDto;

namespace CommerceCore.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
    {
        return Ok(await _categoryService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(Guid id, UpdateCategoryRequestDto categoryDto)
    {
        var updatedCategory = await _categoryService.UpdateAsync(id, categoryDto);

        if (updatedCategory == null)
        {
            return BadRequest();
        }

        return Ok(updatedCategory);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> PostCategory(CreateCategoryRequestDto categoryDto)
    {
        var createdCategoryDto = await _categoryService.CreateAsync(categoryDto);

        if (createdCategoryDto == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDto.Id }, createdCategoryDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var isDeleted = await _categoryService.DeleteAsync(id);

        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
