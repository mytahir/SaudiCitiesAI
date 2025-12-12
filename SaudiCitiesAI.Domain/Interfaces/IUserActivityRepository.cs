using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Domain.Interfaces
{
    public interface IUserActivityRepository
    {
        Task AddSearchAsync(UserSearchHistory history);
        Task AddFavoriteAsync(UserFavorite favorite);
        Task AddAIQueryAsync(UserAIQuery query);
    }
}