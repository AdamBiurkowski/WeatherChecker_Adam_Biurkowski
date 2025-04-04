using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherChecker_Adam_Biurkowski.Intrefaces;

namespace WeatherChecker_Adam_Biurkowski.Services
{
    public class JwtProviderService : IJwtTokenService
    {
        private readonly string _secretKey;

        public JwtProviderService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(string email)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourApp",
                audience: "YourApp",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
