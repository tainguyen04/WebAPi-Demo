using DemoDangTin.EF;
using DemoDangTin.Entities;
using DemoDangTin.Infrastructure;
using DemoDangTin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace DemoDangTin.Repository
{
    public class BaiDangRepository : Repository<BaiDang>, IBaiDangRepository
    {
        public BaiDangRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<BaiDang?>> GetBaiDangByDateAsync(DateTime date)
        {
            return await _entities.Where(d => d.NgayDang.Date == date).ToListAsync();
        }
    }
}
