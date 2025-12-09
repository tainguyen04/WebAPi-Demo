using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;
using DemoDangTin.Infrastructure;

namespace DemoDangTin.Interface.Repository
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
