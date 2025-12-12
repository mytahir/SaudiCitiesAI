using SaudiCitiesAI.Application.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> LoginAsync(UserLoginRequest request, CancellationToken ct = default);
        Task<UserResponse> RegisterAsync(UserRegisterRequest request, CancellationToken ct = default);
        Task<UserDashboardResponse?> GetDashboardAsync(string userId, CancellationToken ct = default);
    }
}