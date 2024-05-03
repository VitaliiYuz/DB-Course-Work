using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using TileShop.Application.Settings;
using Microsoft.AspNetCore.Http;
using TileShop.Domain.Repositories;
using TileShop.Application.Services.Interfaces;

namespace TileShop.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IProductRepository _productRepository;

        public ImageService(IOptions<CloudinarySettings> config, IProductRepository productRepository)
        {
            var acc = new Account
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            };
            _cloudinary = new Cloudinary(acc);
            _productRepository = productRepository;
        }

        public async Task<string> AddPhotoAsync(IFormFile file, int id)
        {
            var uploadResult = new ImageUploadResult();
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
                var product = await _productRepository.GetByIdAsync(id);
                product.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
                await _productRepository.UpdateAsync(product);
                return "Photo updated";
            }
            else
            {
                return "File not selected";
            }
        }
    }
}
