using CloudinaryDotNet.Actions;
using DemoDangTin.DTO.Response;

namespace DemoDangTin.Interface.Service
{
    public interface IPhotoService
    {
        Task<PhotoResponse> AddPhotoAsync(IFormFile file);
    }
}
