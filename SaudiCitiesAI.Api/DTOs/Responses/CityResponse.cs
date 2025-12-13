using System;

namespace SaudiCitiesAI.Api.DTOs.Responses
{
    public class CityResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}