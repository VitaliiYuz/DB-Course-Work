using AutoMapper;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<List<ReviewDto>> GetProductReviewsAsync(int productId)
    {
        var reviews = await _reviewRepository.GetByCreatedDate(productId);
        return _mapper.Map<List<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> CreateReviewAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        if(await _reviewRepository.IsReviewExist(reviewDto.UserId, reviewDto.ProductId))
        {
            throw new Exception("Review already exist");
        }
        var result = await _reviewRepository.CreateAsync(review);
        return _mapper.Map<ReviewDto>(result);
    }

    public async Task UpdateReviewAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        await _reviewRepository.UpdateAsync(review);
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        if(review is not null)
            await _reviewRepository.DeleteAsync(id);
    } 
}
