using AutoMapper;
using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;
using DemoDangTin.Interface.Repository;
using DemoDangTin.Interface.Service;
using System.Linq.Expressions;

namespace DemoDangTin.Services
{
    public class BaiDangService : IBaiDangService
    {
        private IBaiDangRepository _baiDangRepository;
        private IMapper _mapper;
        private readonly ILogger<BaiDangService> _logger;
        public BaiDangService(IBaiDangRepository baiDangRepository, IMapper mapper, ILogger<BaiDangService> logger)
        {
            _baiDangRepository = baiDangRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BaiDangResponse> CreateBaiDangAsync(CreateBaiDangRequest request)
        {
            try
            {
                var baiDang = _mapper.Map<BaiDang>(request);
                baiDang.NgayDang = DateTime.Now;
                await _baiDangRepository.CreateAsync(baiDang);
                await _baiDangRepository.SaveChangeAsync();
                var baiDangResponse = _mapper.Map<BaiDangResponse>(baiDang);
                return baiDangResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi tạo bài đăng: {ex.Message}");
                return null!;
            }
        }

        public async Task DeleteBaiDangAsync(int id)
        {
            try
            {
                var baiDang = await _baiDangRepository.GetByIdAsync(id);
                if (baiDang == null) _logger.LogInformation($"Không tìm thấy bài đăng của {id}");
                else
                {
                    _baiDangRepository.Delete(baiDang);
                    await _baiDangRepository.SaveChangeAsync();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Lỗi khi xóa bài đăng với id {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<BaiDang>> GetAllAsync()
        {
            var baiDangs = await _baiDangRepository.GetAllAsync();
            if (!baiDangs.Any()) _logger.LogInformation("Không tìm thấy bài đăng");
            return baiDangs;
        }

        public async Task<BaiDangResponse?> GetBaiDangByIdAsync(int id)
        {
            var baiDang = await _baiDangRepository.GetByIdAsync(id);
            if(baiDang == null) _logger.LogInformation($"Không tìm thấy bài đăng theo {id}");
            var baiDangResponse = _mapper.Map<BaiDangResponse>(baiDang);
            return baiDangResponse;
        }

        public async Task<BaiDangResponse?> UpdateBaiDangAsync(int id, UpdateBaiDangRequest request)
        {
            try
            {
                var baiDang = await _baiDangRepository.GetByIdAsync(id);
                if (baiDang == null)
                {
                    _logger.LogInformation($"Không tìm thấy bài đăng của {id}");
                    return null;
                }
                else
                {
                    _mapper.Map(request, baiDang);
                    _baiDangRepository.Update(baiDang);
                    await _baiDangRepository.SaveChangeAsync();
                    var baiDangResponse = _mapper.Map<BaiDangResponse>(baiDang);
                    return baiDangResponse;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Lỗi khi cập nhật bài đăng: {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<BaiDangResponse>?> GetBaiDangByDateAsync(DateTime date)
        {
            try
            {
                var baiDangs = await _baiDangRepository.GetBaiDangByDateAsync(date);
                if (!baiDangs.Any())
                    _logger.LogInformation($"Không tìm thấy bài đăng theo ngày {date}");

                var baiDangResponse = _mapper.Map<IEnumerable<BaiDangResponse>>(baiDangs);
                return baiDangResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy bài đăng theo ngày: {ex.Message}");
                return null;
            }
        }
    }
}
