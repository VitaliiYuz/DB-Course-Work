using Microsoft.AspNetCore.Mvc;
using TileShop.Application.Services.Interfaces;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IProductService _productService;

    public ImagesController(IConfiguration configuration, IProductService productService)
    {
        _configuration = configuration;
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImage(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product.ImageUrl);
    }

    private string GetImageMimeTypeFromFileExtension(string extension)
        {
            string mimetype = extension switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                ".tiff" => "image/tiff",
                ".wmf" => "image/wmf",
                ".jp2" => "image/jp2",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream",
            };
            return mimetype;
        }
}