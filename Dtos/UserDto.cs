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


    }

}