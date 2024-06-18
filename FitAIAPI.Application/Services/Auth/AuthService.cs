using AutoMapper;
using FitAIAPI.Application.Auth;
using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces;
using FitAIAPI.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitAIAPI.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;

        public AuthService(IOptionsMonitor<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.CurrentValue;
        }

        public Claim[] GetClaims(User user)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}") ,
            new Claim("is_first_login", user.IsFirstLogin.ToString())
        };

            return claims;
        }

        public string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }



    }
}
