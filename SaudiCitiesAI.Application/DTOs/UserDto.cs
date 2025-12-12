using System;
using System.Collections.Generic;

namespace SaudiCitiesAI.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public List<UserActivityDto> Activity { get; set; } = new();
    }
}