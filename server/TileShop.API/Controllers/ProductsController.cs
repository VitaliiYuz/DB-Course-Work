using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileShop.API.Products.Requests;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper, IImageService imageService)
    {
        _productService = productService;
        _mapper = mapper;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<ActionResult<ProductDto>> GetProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var productDto = _mapper.Map<ProductDto>(request);
        var created = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(CreateProduct), created);
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
    {
        var productDto = _mapper.Map<ProductDto>(request);
        await _productService.UpdateProductAsync(productDto);
        return NoContent();
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateImage(int id, IFormFile file)
    {
        if(file != null)
        {
            await _imageService.AddPhotoAsync(file, id);
        }
        return NoContent();
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
