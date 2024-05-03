using AutoMapper;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IMapper _mapper;

    public RatingService(IRatingRepository ratingRepository, IMapper mapper)
    {
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }

    public async Task<RatingDto> CreateAsync(RatingDto ratingDto)
    {
        var rating = _mapper.Map<Rating>(ratingDto);
        var createdRating = await _ratingRepository.CreateAsync(rating);
        return _mapper.Map<RatingDto>(createdRating);
    }

    public async Task UpdateAsync(RatingDto ratingDto)
    {
        var rating = _mapper.Map<Rating>(ratingDto);
        await _ratingRepository.UpdateAsync(rating);
    }

    public async Task DeleteAsync(int id)
    {
        await _ratingRepository.DeleteAsync(id);
    }
}
