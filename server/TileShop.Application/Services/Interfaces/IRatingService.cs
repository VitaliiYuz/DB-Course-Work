using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IRatingService
{
    Task<RatingDto> CreateAsync(RatingDto ratingDto);

    Task UpdateAsync(RatingDto ratingDto);

    Task DeleteAsync(int id);
}
