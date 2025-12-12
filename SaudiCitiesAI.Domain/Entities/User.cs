using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string ApiKeyHash { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<UserSearchHistory> _searchHistory = new();
        public IReadOnlyCollection<UserSearchHistory> SearchHistory => _searchHistory;

        private readonly List<UserFavorite> _favorites = new();
        public IReadOnlyCollection<UserFavorite> Favorites => _favorites;

        private readonly List<UserAIQuery> _aiQueries = new();
        public IReadOnlyCollection<UserAIQuery> AIQueries => _aiQueries;

        private User() { } // EF Core only

        public User(string email, string apiKeyHash)
        {
            Id = Guid.NewGuid();
            Email = email.Trim().ToLower();
            ApiKeyHash = apiKeyHash;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateApiKeyHash(string newHash)
        {
            ApiKeyHash = newHash;
        }

        public void AddSearch(UserSearchHistory history)
        {
            _searchHistory.Add(history);
        }

        public void AddFavorite(UserFavorite fav)
        {
            _favorites.Add(fav);
        }

        public void AddAIQuery(UserAIQuery query)
        {
            _aiQueries.Add(query);
        }
    }
}

