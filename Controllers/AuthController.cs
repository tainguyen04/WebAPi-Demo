using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoDangTin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("log-in")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _authService.Authenticate(request);
            if(!response.Authenticated)
                return Unauthorized(response);
            return Ok(response);
        }
    }
}
