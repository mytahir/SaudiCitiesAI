using System;

namespace SaudiCitiesAI.Application.DTOs
{
    public class Vision2030Dto
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }

        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;

        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
    }
}