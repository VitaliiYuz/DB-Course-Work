using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileShop.API.Categories.Requests;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CategoryDto>> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var categoryDto = _mapper.Map<CategoryDto>(request);
        var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
        return CreatedAtAction(nameof(CreateCategory), createdCategory);
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var categoryDto = _mapper.Map<CategoryDto>(request);
        await _categoryService.UpdateCategoryAsync(categoryDto);
        return NoContent();
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
