using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

namespace MyGraphqlApp.Utils
{

    public class JwtUtils
    {


      
        private readonly SymmetricSecurityKey _signingKey;





        public JwtUtils()
        {
            Env.Load();
            string? secretKey = Environment.GetEnvironmentVariable("JWT_KEY");


            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        }

        // Generate JWT token with email as subject
        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // 1 hour expiry
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Extract email from token
        public string ExtractEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);

            return principal.Identity?.Name ?? string.Empty;
        }
    }
}
