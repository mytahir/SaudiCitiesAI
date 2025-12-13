using SaudiCitiesAI.Application.DTOs.Users;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SaudiCitiesAI.Application.Services
{
    public class UserService : IUserService
    {
            private readonly IUserRepository _users;

            public UserService(IUserRepository users)
            {
                _users = users;
            }

            public async Task<UserResponse> RegisterAsync(
                UserRegisterRequest request,
                CancellationToken ct = default)
            {
                var apiKey = GenerateApiKey();
                var apiKeyHash = ComputeHash(apiKey);

                var user = new User(request.Email, apiKeyHash);

                await _users.AddAsync(user, ct);
                await _users.SaveChangesAsync(ct);

                return new UserResponse
                {
                    UserId = user.Id.ToString(),
                    Email = user.Email,
                    ApiKey = apiKey // shown ONCE
                };
            }

            public async Task<UserDashboardResponse?> GetDashboardAsync(
                Guid userId,
                CancellationToken ct = default)
            {
                var user = await _users.GetByIdAsync(userId, ct);
                if (user == null)
                    return null;

                return new UserDashboardResponse
                {
                    Email = user.Email,
                    TotalQueries = user.TotalQueries,
                    RegisteredAt = user.CreatedAt
                };
            }

            private string GenerateApiKey()
                => Guid.NewGuid().ToString("N");

            private string ComputeHash(string value)
            {
                using var sha = SHA256.Create();
                return Convert.ToBase64String(
                    sha.ComputeHash(Encoding.UTF8.GetBytes(value)));
            }

    }
}