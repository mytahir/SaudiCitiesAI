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

        public async Task<UserResponse?> LoginAsync(UserLoginRequest request, CancellationToken ct = default)
        {
            var user = await _users.GetByEmailAsync(request.Email, ct);
            if (user == null)
                return null;

            if (!VerifyPassword(request.Password, user.PasswordHash))
                return null;

            return new UserResponse
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                ApiKey = user.ApiKey // returned only on login
            };
        }

        public async Task<UserResponse> RegisterAsync(UserRegisterRequest request, CancellationToken ct = default)
        {
            var apiKey = GenerateApiKey();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                ApiKey = apiKey,                     // returned to user
                ApiKeyHash = ComputeHash(apiKey)     // stored hashed
            };


            await _users.AddAsync(user, ct);
            await _users.SaveChangesAsync(ct);

            return new UserResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                ApiKey = user.ApiKey
            };
        }

        public async Task<UserDashboardResponse?> GetDashboardAsync(string userId, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(Guid.Parse(userId), ct);
            if (user == null)
                return null;

            return new UserDashboardResponse
            {
                Email = user.Email,
                TotalQueries = user.TotalQueries,
                RegisteredAt = user.CreatedAt
            };
        }

        private string HashPassword(string value)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        private bool VerifyPassword(string password, string storedHash)
            => HashPassword(password) == storedHash;

        private string GenerateApiKey() => Guid.NewGuid().ToString("N");
        private string ComputeHash(string key) => HashPassword(key);
    }
}