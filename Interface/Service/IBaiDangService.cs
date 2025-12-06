using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;

namespace DemoDangTin.Interface.Service
{
    public interface IBaiDangService
    {
        Task<IEnumerable<BaiDang>> GetAllAsync();
        Task<BaiDangResponse?> GetBaiDangByIdAsync(int id);
        Task<BaiDangResponse> CreateBaiDangAsync(CreateBaiDangRequest request);
        Task<BaiDangResponse?> UpdateBaiDangAsync(int id,UpdateBaiDangRequest request);
        Task DeleteBaiDangAsync(int id);
        Task<IEnumerable<BaiDangResponse>?> GetBaiDangByDateAsync(DateTime date);
    }
}
