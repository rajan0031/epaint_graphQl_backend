using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyGraphqlApp.Utils
{
    public class JwtUtils
    {
        private const string SECRET_KEY = "mysecretkeymysecretkeymysecretkey"; // same as Java
        private readonly SymmetricSecurityKey _signingKey;

        public JwtUtils()
        {
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
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
