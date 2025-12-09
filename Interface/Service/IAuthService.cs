using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;

namespace DemoDangTin.Interface.Service
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        string GenerateToken(User user);
    }
}
