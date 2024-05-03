using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileShop.API.Reviews.Requests;
using TileShop.Application.Services;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RatingController : BaseController
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateRating(int productId, int score)
    {
        var result = await _ratingService.CreateAsync(new RatingDto
        {
            ProductId = productId,
            Score = score,
            UserId = int.Parse(UserId)
        });
        return CreatedAtAction(nameof(CreateRating), result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReviewAsync(int id, int productId, int score)
    {
        await _ratingService.UpdateAsync(new RatingDto
        {
            Id = id,
            Score = score,
            ProductId = productId
        });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        await _ratingService.DeleteAsync(id);
        return NoContent();
    }
}
