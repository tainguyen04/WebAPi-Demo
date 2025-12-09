using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoDangTin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiDangController : ControllerBase
    {
        private readonly IBaiDangService _baiDangService;
        public BaiDangController(IBaiDangService baiDangService)
        {
            _baiDangService = baiDangService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BaiDangResponse>>> GetAllAsync()
        {
            try
            {
                var baiDangs = await _baiDangService.GetAllAsync();
                if (baiDangs == null || !baiDangs.Any())
                    return NotFound("Không tìm thấy bài đăng");
                return Ok(baiDangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BaiDangResponse>> GetBaiDangByIdAsync([FromRoute] int id)
        {
            try
            {
                var baiDang = await _baiDangService.GetBaiDangByIdAsync(id);
                if (baiDang == null)
                    return NotFound($"Không tìm thấy bài đăng với id {id}");
                return Ok(baiDang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<BaiDangResponse>>> GetBaiDangByDateAsync([FromRoute] DateTime date)
        {
            try
            {
                var baiDangs = await _baiDangService.GetBaiDangByDateAsync(date);
                if(baiDangs == null || !baiDangs.Any())
                    return NotFound($"Không tìm thấy bài đăng vào ngày {date.ToShortDateString()}");
                return Ok(baiDangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateBaiDangAsync([FromBody] CreateBaiDangRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Dữ liệu không hợp lệ");
                await _baiDangService.CreateBaiDangAsync(request);
                return Ok(new { message = "Tạo bài đặng thành công"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBaiDangAsync([FromRoute] int id, [FromBody] UpdateBaiDangRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Dữ liệu không hợp lệ");
                var baiDang = await _baiDangService.GetBaiDangByIdAsync(id);
                if (baiDang == null)
                    return NotFound($"Không tìm thấy bài đăng với id {id}");
                await _baiDangService.UpdateBaiDangAsync(id, request);
                return Ok(new { message = $"Cập nhật bài đăng của {id} thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaiDangAsync([FromRoute] int id)
        {
            try
            {
                var baiDang = await _baiDangService.GetBaiDangByIdAsync(id);
                if(baiDang == null)
                    return NotFound($"Không tìm thấy bài đăng với id {id}");
                await _baiDangService.DeleteBaiDangAsync(id);
                return Ok(new { message = $"Xóa bài đăng của {id} thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

    }
}
