namespace MyGraphqlApp.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }

        public string ? PhoneNumber { get; set; }



        public int Role { get; set; }

        public int LoginFlag { get; set; }

    }
}
