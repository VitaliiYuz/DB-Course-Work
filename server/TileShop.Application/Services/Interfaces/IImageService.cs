using Microsoft.AspNetCore.Http;

namespace TileShop.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> AddPhotoAsync(IFormFile file, int id);
    }
}
