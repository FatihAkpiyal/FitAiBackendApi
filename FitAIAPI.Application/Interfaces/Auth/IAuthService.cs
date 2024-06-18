using FitAIAPI.Application.DTOs;
using FitAIAPI.Domain.Entities;
using System.Security.Claims;

namespace FitAIAPI.Application.Interfaces
{
    public interface IAuthService
    {
        string Token(User user);
        Claim[] GetClaims(User user);
    }
}
