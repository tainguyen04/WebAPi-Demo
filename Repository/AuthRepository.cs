using DemoDangTin.DTO.Response;
using DemoDangTin.EF;
using DemoDangTin.Entities;
using DemoDangTin.Infrastructure;
using DemoDangTin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace DemoDangTin.Repository
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return  await _entities.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
