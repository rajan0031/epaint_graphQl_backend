using System.Collections.Generic;

namespace MyGraphqlApp.Model
{
    public class Employee
    {
        public int Id { get; set; }

        // Required fields
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        // Array of skills
        public List<string>? Skills { get; set; }

        // Experience in hours
        public int HoursOfExperience { get; set; }

        // Number of projects completed
        public int ProjectsCompleted { get; set; }

        // Foreign key to Painter
        public int EmployerId { get; set; }

        public Painter? Employer { get; set; }
    }
}
