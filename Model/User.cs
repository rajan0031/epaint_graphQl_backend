namespace MyGraphqlApp.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }



        public int Role { get; set; }

        public int LoginFlag { get; set; }

        public int IsEmailVerified { get; set; } = 0;

        public string? EmailOtp { get; set; } = string.Empty;

        public int IsPhoneNumberVerified { get; set; } = 0;

        public string? PhoneNumberOtp { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
