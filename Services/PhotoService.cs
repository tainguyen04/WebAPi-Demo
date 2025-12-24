using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DemoDangTin.DTO.Response;
using DemoDangTin.Interface.Service;
using System.Net.WebSockets;

namespace DemoDangTin.Services
{
    public class PhotoService : IPhotoService
    {
        public readonly Cloudinary _cloudinary;
        public PhotoService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<PhotoResponse> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return new PhotoResponse { PhotoUrl = uploadResult.SecureUrl.AbsoluteUri, PublicId = uploadResult.PublicId};
        }
    }
}
