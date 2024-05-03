using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IReviewService
{
    Task<List<ReviewDto>> GetProductReviewsAsync(int productId);
    Task<ReviewDto> CreateReviewAsync(ReviewDto reviewDto);
    Task UpdateReviewAsync(ReviewDto reviewDto);
    Task DeleteReviewAsync(int id);
}
