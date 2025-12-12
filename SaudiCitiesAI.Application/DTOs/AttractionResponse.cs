using System;

namespace SaudiCitiesAI.Application.DTOs
{
    public class AttractionResponse
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
