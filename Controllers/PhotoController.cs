using DemoDangTin.DTO.Response;
using DemoDangTin.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoDangTin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        [HttpPost("upload-image")]
        public async Task<ActionResult<PhotoResponse>> UploadImage(IFormFile file)
        {
            if( file is null) return BadRequest("No file uploaded.");
            var result = await _photoService.AddPhotoAsync(file);

            return Ok(result);
        }
    }
}
