
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyGraphqlApp.Model;


namespace MyGraphqlApp.Utils
{

    public class JwtUtils
    {



        private readonly SymmetricSecurityKey _signingKey;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public JwtUtils(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;

            Env.Load();
            string? secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Please make sure your env file have JWT_KEY  as a variable ");
            }

            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        }

        // Generate JWT token with email as subject
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
               new Claim("Id",user.Id.ToString()),
               new Claim ("Name" ,user.Name),
               new Claim ("UserName" ,user.UserName),
               new Claim ("Email" ,user.Email),
               new Claim ("PhoneNumber" ,user.PhoneNumber!),
               new Claim ("Role" ,user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // 1 hour expiry
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Extract all the details from the token so that it will , remove the problem of the sending id name etc... for a logged in user , also i wm used this thing for the role validations 
        public User ExtractLoggedInUserDetails()
        {
            var authHeader = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].ToString();
            var  token  = authHeader.StartsWith("Bearer ")
                ? authHeader.Substring("Bearer ".Length)
                : authHeader;

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);


            var claims = principal.Claims;

            var user = new User
            {

                Id = int.Parse(claims.First(c => c.Type == "Id").Value),
                Name = claims.First(c => c.Type == "Name").Value,
                UserName = claims.First(c => c.Type == "UserName").Value,
                Email = claims.First(c => c.Type == "Email").Value,
                PhoneNumber = claims.First(c => c.Type == "PhoneNumber")?.Value,
                Role = int.Parse(claims.First(c => c.Type == "Role").Value),

            };

            return user;
        }


        
    }
}
