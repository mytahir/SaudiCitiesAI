using System;
using System.Collections.Generic;

using SaudiCitiesAI.Domain.Enums;
using SaudiCitiesAI.Domain.ValueObjects;

namespace SaudiCitiesAI.Domain.Entities
{
    public class Attraction
    {
        public Guid Id { get; private set; }
        public Guid CityId { get; private set; }
        public string Name { get; private set; }
        public AttractionCategory Category { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public string Description { get; set; }

        private Attraction() { }

        public Attraction(Guid cityId, string name, AttractionCategory category, Coordinates coordinates)
        {
            Id = Guid.NewGuid();
            CityId = cityId;
            Name = name;
            Category = category;
            Coordinates = coordinates;
        }
    }
}

