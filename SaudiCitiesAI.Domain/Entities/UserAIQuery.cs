using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Entities
{
    public class UserAIQuery
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Prompt { get; private set; }
        public string ResponseSummary { get; private set; }
        public DateTime Timestamp { get; private set; }

        private UserAIQuery() { }

        public UserAIQuery(Guid userId, string prompt, string responseSummary)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Prompt = prompt;
            ResponseSummary = responseSummary;
            Timestamp = DateTime.UtcNow;
        }
    }
}

