using System;
using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Domain.Entities
{
    public class Vision2030Focus
    {
        public Guid Id { get; private set; }
        public Guid CityId { get; private set; }   // <-- FIXED
        public Vision2030Category Category { get; private set; }
        public string Description { get; private set; }

        private Vision2030Focus() { }

        public Vision2030Focus(Guid cityId, Vision2030Category category, string description)
        {
            Id = Guid.NewGuid();
            CityId = cityId;
            Category = category;
            Description = description;
        }
    }
}