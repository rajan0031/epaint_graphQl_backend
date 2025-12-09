using System;
using System.Collections.Generic;

namespace MyGraphqlApp.Model
{
    public class Painter
    {
        public int Id { get; set; }

        // Painter/Decorator’s name or business name
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        // Array of service types (Painter, Decorator, Wallpaper Specialist, etc.)
        public List<string>? ServiceTypes { get; set; }

        public int ExperienceYears { get; set; }

        public double Rating { get; set; }

        // Array of pin codes where this painter provides service
        public List<string>? PinCodes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // One Painter can have many employees
        public List<Employee>? Employees { get; set; }
    }
}
