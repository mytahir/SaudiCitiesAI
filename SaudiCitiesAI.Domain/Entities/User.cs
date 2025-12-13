using System;
using System.Collections.Generic;

namespace SaudiCitiesAI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string ApiKeyHash { get; private set; }
        public int TotalQueries { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Navigation collections
        private readonly List<UserSearchHistory> _searchHistory = new();
        public IReadOnlyCollection<UserSearchHistory> SearchHistory => _searchHistory;

        private readonly List<UserFavorite> _favorites = new();
        public IReadOnlyCollection<UserFavorite> Favorites => _favorites;

        private readonly List<UserAIQuery> _aiQueries = new();
        public IReadOnlyCollection<UserAIQuery> AIQueries => _aiQueries;

        protected User() { } // Required by EF Core

        // ✅ This is the constructor to use in code
        public User(string email, string apiKeyHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            ApiKeyHash = apiKeyHash;
            CreatedAt = DateTime.UtcNow; // Sets default value in code
        }

        // Business logic methods
        public void IncrementQueries() => TotalQueries++;
        public void AddSearchHistory(UserSearchHistory history) => _searchHistory.Add(history);
        public void AddFavorite(UserFavorite favorite) => _favorites.Add(favorite);
        public void AddAIQuery(UserAIQuery query) => _aiQueries.Add(query);
    }
}