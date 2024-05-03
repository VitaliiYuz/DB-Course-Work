using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileShop.API.Reviews.Requests;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;

    public ReviewsController(IReviewService reviewService, IMapper mapper)
    {
        _reviewService = reviewService;
        _mapper = mapper;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductReviews(int productId)
    {
        var reviews = await _reviewService.GetProductReviewsAsync(productId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewRequest request)
    {
        var review = _mapper.Map<ReviewDto>(request);

        var result = await _reviewService.CreateReviewAsync(review);
        return CreatedAtAction(nameof(CreateReview), result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReviewAsync(UpdateReviewRequest request)
    {
        var review = _mapper.Map<ReviewDto>(request);
        await _reviewService.UpdateReviewAsync(review);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        await _reviewService.DeleteReviewAsync(id);
        return NoContent();
    }  
}
