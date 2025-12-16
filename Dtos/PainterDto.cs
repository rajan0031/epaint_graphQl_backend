
namespace MyGraphqlApp.dtos.PainterDto
{


    public class PainterDto
    {

        public class PainterResponse
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;

            public string UserName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

           

            public string? PhoneNumber { get; set; }

            public string? Address { get; set; }

            public string? City { get; set; }


            public List<string>? ServiceTypes { get; set; }

            public int ExperienceYears { get; set; }

            public double Rating { get; set; }

            // Array of pin codes where this painter provides service
            public List<string>? PinCodes { get; set; }

            // login for the first time flage 

            // public int LoginFlag { get; set; }

            public bool IsDeleted { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }

            public DateTime? DeletedAt { get; set; }

        }


    }


}