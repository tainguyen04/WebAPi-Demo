using AutoMapper;
using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;
using DemoDangTin.Interface.Repository;
using DemoDangTin.Interface.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DemoDangTin.Services
{
    public class AuthenticationService : IAuthService
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(IAuthRepository authRepository, ILogger<AuthenticationService> logger, IConfiguration configuration, SymmetricSecurityKey securityKey)
        {
            _authRepository = authRepository;
            _logger = logger;
            _configuration = configuration;
            _securityKey = securityKey;
        }
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var user = await _authRepository.GetUserByUsernameAsync(request.UserName);
            if (user is null || user.Password is null)
            {
                _logger.LogWarning("Authentication failed. User not found: {UserName}", request.UserName);
                return new AuthenticationResponse { Authenticated = false };
            }
            bool authenticated = BCryptNet.Verify(request.Password, user.Password);
            if (!authenticated)
            {
                _logger.LogWarning("Authentication failed. Invalid password for user: {UserName}", request.UserName);
                return new AuthenticationResponse { Authenticated = false };
            }

            var token = GenerateToken(user);
            return new AuthenticationResponse
            {
                Authenticated = authenticated,
                Token = token
            };

        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };
            
            var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
