using DemoDangTin.Entities;
using DemoDangTin.Infrastructure;

namespace DemoDangTin.Interface.Repository
{
    public interface IBaiDangRepository : IRepository<BaiDang>
    {
        Task<IEnumerable<BaiDang?>> GetBaiDangByDateAsync(DateTime date);
    }
}
