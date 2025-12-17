using System.Collections.Generic;

namespace SaudiCitiesAI.Application.Utils
{
    public static class SaudiCityNameMapper
    {
        // English name variants → Official Arabic name
        private static readonly Dictionary<string, string> EnglishToArabic = new()
        {
            { "Riyadh", "الرياض" },
            { "Al Riyadh", "الرياض" },
            { "Jeddah", "جدة" },
            { "Jedda", "جدة" },
            { "Dammam", "الدمام" },
            { "Al Khobar", "الخبر" },
            { "Khobar", "الخبر" },
            { "Mecca", "مكة المكرمة" },
            { "Makkah", "مكة المكرمة" },
            { "Medina", "المدينة المنورة" },
            { "Al Madinah", "المدينة المنورة" }
        };

        /// <summary>
        /// Returns the official Arabic name for a city given an English variant.
        /// If not found, returns the original input.
        /// </summary>
        public static string GetArabicName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return EnglishToArabic.TryGetValue(input.Trim(), out var arabic)
                ? arabic
                : input.Trim();
        }
    }
}