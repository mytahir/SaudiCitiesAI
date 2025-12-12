using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Entities
{
    public class UserFavorite
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string ItemId { get; private set; }   // CityId or AttractionId
        public string ItemType { get; private set; } // "City" / "Attraction"
        public DateTime CreatedAt { get; private set; }

        private UserFavorite() { }

        public UserFavorite(Guid userId, string itemId, string itemType)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ItemId = itemId;
            ItemType = itemType;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

