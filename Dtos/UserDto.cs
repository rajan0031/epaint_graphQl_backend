using MyGraphqlApp.Model;

namespace MyGraphqlApp.dtos
{

    public class UserDto
    {

        public class loginDto
        {
            public string? email { get; set; }
            public string? password { get; set; }
        }

        public class LoginResponse
        {
            public User? User { get; set; }
            public string? Token { get; set; }

            public string? Message { get; set; }
        }


        public class ChangePasswordDto
        {
            public int id { get; set; }
            public string? password { get; set; }
            public string? newPassword { get; set; }
        }

        // dto for the get all user 
        public class GetAllUserDto
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string UserName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;



            public string? PhoneNumber { get; set; }



            public int Role { get; set; }



            public bool IsDeleted { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }

            public DateTime? DeletedAt { get; set; }
        }


    }

}