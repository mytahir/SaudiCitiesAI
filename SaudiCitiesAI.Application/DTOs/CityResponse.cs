using System;
using System.Collections.Generic;

namespace SaudiCitiesAI.Application.DTOs
{
    public class CityResponse
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;

        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public List<AttractionResponse> Attractions { get; set; } = new();
        public List<Vision2030Dto> VisionFocus { get; set; } = new();
    }
}