using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Entities
{
    public class UserSearchHistory
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Query { get; private set; }
        public DateTime Timestamp { get; private set; }

        private UserSearchHistory() { }

        public UserSearchHistory(Guid userId, string query)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Query = query;
            Timestamp = DateTime.UtcNow;
        }
    }
}

