using SaudiCitiesAI.Application.DTOs.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface IUserService
    {
        // Register user and issue API key (shown once)
        Task<UserResponse> RegisterAsync(
            UserRegisterRequest request,
            CancellationToken ct = default);

        // User dashboard (authenticated via API key)
        Task<UserDashboardResponse?> GetDashboardAsync(
            Guid userId,
            CancellationToken ct = default);
    }
}